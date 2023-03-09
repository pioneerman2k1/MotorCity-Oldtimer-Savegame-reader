namespace SCR_Ausleser_Array
{
    internal static class Program
    {
        static public string DateiName;
        static public string SaveName;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]

        public static byte[] read_file_bytearray() // OPEN DIALOG UND EINLESEN DES FILES AN DER ENTSPRECHENDEN STELLE
        {
            byte[] readFile = { };
            

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "*.*|";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filePath = openFileDialog.FileName;
                    readFile = File.ReadAllBytes(filePath).ToArray();
                    
                }
                DateiName = openFileDialog.SafeFileName;
                SaveName = openFileDialog.FileName;

                return readFile; // RÜCKGABE DES BYTE ARRAYS
            
            }

        }

        [STAThreadAttribute] // WICHTIG FÜR OPEN DIALOG
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}