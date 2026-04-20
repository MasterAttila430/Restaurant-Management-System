using System;
using System.Windows.Forms;

namespace Ettermek
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                // 1. Elindítjuk a Login ablakot
                LoginForm login = new LoginForm();
                DialogResult loginResult = login.ShowDialog();

                // 2. Ha sikeres volt a belépés (OK-t küldött vissza)
                if (loginResult == DialogResult.OK)
                {
                    // Lekérjük a Login-tól, hogy ki lépett be
                    // (Ezt a két property-t majd hozzáadjuk a LoginForm-hoz a következő lépésben!)
                    int roleId = login.LoggedInRoleId;
                    string userName = login.LoggedInUserName;

                    // 3. Elindítjuk a főablakot
                    Form1 mainForm = new Form1(roleId, userName);
                    DialogResult mainResult = mainForm.ShowDialog();

                    // 4. Ha a főablakból Kijelentkezéssel tértünk vissza (Retry), a ciklus újraindul
                    if (mainResult == DialogResult.Retry)
                    {
                        continue;
                    }
                    else
                    {
                        // Ha simán bezárta (Cancel/None), akkor kilépünk a programból
                        break;
                    }
                }
                else
                {
                    // Ha a Login ablakot bezárták belépés nélkül
                    break;
                }
            }
        }
    }
}
