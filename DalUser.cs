using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Ettermek
{
    public class DalUser : DAL
    {
        public DalUser() : base() { }

        // Regisztráció a tárolt eljárással
        public void RegisterUser(string username, string password, int roleId)
        {
            string err = "";
            // 1. Só generálása és jelszó hash-elése
            string salt = PasswordHelper.GenerateSalt();
            string hash = PasswordHelper.HashPassword(password, salt);

            try
            {
                OpenConnection(ref err);
                using (SqlCommand cmd = new SqlCommand("sp_User_Register", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nev", username);
                    cmd.Parameters.AddWithValue("@Hash", hash);
                    cmd.Parameters.AddWithValue("@Salt", salt);
                    cmd.Parameters.AddWithValue("@CsoportID", roleId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Ha a tárolt eljárás dobta a hibát (pl. RAISERROR)
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Bejelentkezés ellenőrzése
        public int? ValidateUser(string username, string password)
        {
            string err = "";
            try
            {
                OpenConnection(ref err);
                // Lekérjük a hash-t és a sót a név alapján
                string query = "SELECT JelszoHash, JelszoSalt, CsoportID FROM Felhasznalok WHERE FelhasznaloNev = @Nev";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Nev", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["JelszoHash"].ToString();
                            string storedSalt = reader["JelszoSalt"].ToString();
                            int roleId = Convert.ToInt32(reader["CsoportID"]);

                            if (PasswordHelper.VerifyPassword(password, storedHash, storedSalt))
                            {
                                return roleId; // Sikeres, visszaadjuk a szerepkört
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Adatbázis hiba: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return null; // Sikertelen belépés (nincs ilyen user vagy rossz jelszó)
        }
    }
}
