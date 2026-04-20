using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Ettermek
{
    public class MunkasDTO
    {
        public int MunkasID { get; set; }
        public string MunkasNev { get; set; }
        public decimal Fizetes { get; set; }
        public string Beosztas { get; set; }
        public string CNP { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class DalMunkasok : DAL
    {
        // Orszagok lekerese ComboBox-hoz
        public DataSet GetOrszagokData(ref string errMess)
        {
            string query = "SELECT OrszagID, OrszagNev FROM Orszag ORDER BY OrszagNev";
            return ExecuteDS(query, ref errMess);
        }

        public DataSet GetFilteredMunkasok(int? orszagId, string nevFilter, ref string errMess)
        {
            string query = @"
                SELECT m.MunkasID, m.MunkasNev, m.Fizetes, m.Beosztas, m.CNP,
                       e.Nev AS EtteremNev, o.OrszagNev
                FROM Munkasok m
                INNER JOIN Ettermek e ON m.EtteremID = e.EtteremID
                INNER JOIN Orszag o ON e.OrszagID = o.OrszagID";

            using (SqlCommand command = new SqlCommand())
            {
                var conditions = new List<string>();

                // Orszag szuro
                if (orszagId.HasValue)
                {
                    conditions.Add("o.OrszagID = @OrszagID");
                    command.Parameters.Add("@OrszagID", SqlDbType.Int).Value = orszagId.Value;
                }

                // Nev szuro (LIKE)
                if (!string.IsNullOrEmpty(nevFilter))
                {
                    conditions.Add("m.MunkasNev LIKE @NevFilter");
                    command.Parameters.Add("@NevFilter", SqlDbType.NVarChar, 100).Value = nevFilter + "%";
                }

                // WHERE hozzaadasa ha van feltetel
                if (conditions.Any())
                {
                    query += " WHERE " + string.Join(" AND ", conditions);
                }

                query += " ORDER BY o.OrszagNev, e.Nev, m.MunkasNev";
                command.CommandText = query;

                return ExecuteDS(command, ref errMess);
            }
        }

        // === II. FELADAT - TORLES TRIGGERREL ===

        // Ettermek lekerese ComboBox-hoz
        public DataSet GetEttermekData(ref string errMess)
        {
            string query = "SELECT EtteremID, Nev, Cim FROM Ettermek ORDER BY Nev";
            return ExecuteDS(query, ref errMess);
        }

        // Etterem torlése - paraméterezett (trigger fut le az adatbazisban!)
        public int DeleteEtterem(int etteremID, ref string errMess)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = "DELETE FROM Ettermek WHERE EtteremID = @EtteremID";
                command.Parameters.Add("@EtteremID", SqlDbType.Int).Value = etteremID;
                return ExecuteNonQuery(command, ref errMess);
            }
        }

        // === III. FELADAT - OPTIMISTA KONKURENCIAVEZERLES ===

        // Munkas lekerese ID alapján
        public MunkasDTO GetMunkasByID(int munkasID, ref string errMess)
        {
            MunkasDTO munkas = null;
            SqlDataReader reader = null;

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = @"
                    SELECT MunkasID, MunkasNev, Fizetes, Beosztas, CNP, RowVersion
                    FROM Munkasok
                    WHERE MunkasID = @MunkasID";

                command.Parameters.Add("@MunkasID", SqlDbType.Int).Value = munkasID;

                try
                {
                    reader = ExecuteReader(command, ref errMess);

                    if (reader != null && reader.Read())
                    {
                        munkas = new MunkasDTO
                        {
                            MunkasID = reader.GetInt32(0),
                            MunkasNev = reader.GetString(1),
                            Fizetes = reader.GetDecimal(2),
                            Beosztas = reader.GetString(3),
                            CNP = reader.GetString(4),
                            RowVersion = (byte[])reader["RowVersion"]
                        };
                    }
                }
                finally
                {
                    reader?.Close();
                    CloseConnection();
                }
            }

            return munkas;
        }

        // Fizetes modositása - RowVersion ellenorzésével
        // Ha RowVersion nem egyezik, 0 sort érint (konkurencia probléma)
        public int UpdateMunkasFizetes(int munkasID, decimal ujFizetes, byte[] rowVersion, ref string errMess)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = @"
                    UPDATE Munkasok
                    SET Fizetes = @Fizetes
                    WHERE MunkasID = @MunkasID AND RowVersion = @RowVersion";

                command.Parameters.Add("@Fizetes", SqlDbType.Decimal).Value = ujFizetes;
                command.Parameters.Add("@MunkasID", SqlDbType.Int).Value = munkasID;
                command.Parameters.Add("@RowVersion", SqlDbType.Timestamp).Value = rowVersion;

                return ExecuteNonQuery(command, ref errMess);
            }
        }

        // Összes munkas lekerese - Modositas panel DataGridView-hoz
        public DataSet GetAllMunkasok(ref string errMess)
        {
            string query = @"
                SELECT m.MunkasID, m.MunkasNev, m.Fizetes, m.Beosztas, m.CNP,
                       e.Nev AS EtteremNev
                FROM Munkasok m
                INNER JOIN Ettermek e ON m.EtteremID = e.EtteremID
                ORDER BY m.MunkasNev";

            return ExecuteDS(query, ref errMess);
        }
    }
}