using System.Text;

namespace Savegame-Reader
{
    public partial class Form1 : Form
    {
        byte[] configFile; // GESAMTE CONFIG FILE

        /*
                    In Spielständen (GFX\GAME?.GAM) ist das Geld ab Offset 198H gespeichert;
                    für ein Taschengeld von 1.879.048.191 Dollar tragen Sie FF FF FF 6F ein.

                    Weitere Offsets:
                    Offset	Inhalt	                Empfehlung
                    78H	    Personalkosten	        00 00 00 00 ---- Erledigt

                    44H	    Anzahl Facharbeiter	    0F 27       ---- Erledigt   (10 Bytes Pro Mitarbeiter) 4C0F C205 0000 8200 3C00
                                                                                                           Anz.           Geh. WochenStd.
                    49H     Gehalt Facharbeiter     
                    4BH     Wochenstunden FA

                    4EH	    Anzahl Hilfsarbeiter	0F 27       ---- Erledigt                              0000 0000 0000 6E00 3C00
                    54H     Gehalt Hilfsarbeiter
                    56H     Wochenstunden HA

                    58H	    Anzahl Ingenieure	    0F 27       ---- Erledigt                              1900 0000 0000 C601 3C00
                    5EH     Gehalt Ingenieure       C6 01
                    60H     Wochenstunden Ing.      3C 00

                    62H	    Schulden	            00 00 00 00 ---- Erledigt, wird aber vermutlich nicht richtig ausgelesen.
                    1CAH	Konto	                FF FF FF 7F ---- Erledigt
                    90H	    Ertrag	                FF FF FF 6F ---- Erledigt

                     */

        public string reverse_four_bytes(int Byte0, int Byte1, int Byte2, int Byte3)
        {
            string returnString = string.Empty;

            byte[] Gesamtbytes = { Convert.ToByte(Byte3), Convert.ToByte(Byte2), Convert.ToByte(Byte1), Convert.ToByte(Byte0) };
            string ReversedByteshex = BitConverter.ToString(Gesamtbytes).Replace("-", string.Empty);
            int ReverseddecValue = Convert.ToInt32(ReversedByteshex, 16);
            returnString = ReverseddecValue.ToString();

            return returnString;
        }
        public string Earnings()
        {
            string returnString = string.Empty;
            int[] Ertrag = new int[4];
            // Schulden     Werden falsch ausgelesen?
            for (int i = 0; i < 4; i++)
            {
                Ertrag[i] = configFile[144 + i];

            }
            returnString = reverse_four_bytes(Ertrag[0], Ertrag[1], Ertrag[2], Ertrag[3]) + ",-";

            return returnString;
        }
        public string Depts()
        {
            string returnString = string.Empty;
            int[] Schulden = new int[4];
            string SchuldenString = "";
            // Schulden     Werden falsch ausgelesen?
            for (int i = 0; i < 4; i++)
            {
                Schulden[i] = configFile[98 + i];

            }
            SchuldenString = reverse_four_bytes(Schulden[0], Schulden[1], Schulden[2], Schulden[3]);

            if (SchuldenString == "0")
            {
                returnString = "Keine";
            }
            else
                returnString = (SchuldenString + ",-");

            return returnString;
        }
        public string PlayerName()
        {
            string returnString = string.Empty;

            // Spielernamen auslesen
            for (int i = 0; i < configFile[20]; i++)
            {
                returnString += Convert.ToChar(configFile[21 + i]);  // FÜLLT DAS Jahr ARRAY MIT EXAKT 2 BYTE START BEI INDEX
            }
            return returnString;
        }
        public string FirmName()
        {
            string returnString = string.Empty;

            // Firmennamen auslesen
            for (int i = 0; i < configFile[36]; i++)
            {
                returnString += Convert.ToChar(configFile[37 + i]);  // FÜLLT DAS Jahr ARRAY MIT EXAKT 2 BYTE START BEI INDEX
            }
            return returnString;
        }
        public string capital()
        {
            string returnString = string.Empty;
            int[] Kapital = new int[4];
            for (int i = 0; i < 4; i++)
            {
                Kapital[i] = configFile[144 + i];

            }
            returnString = reverse_four_bytes(Kapital[0], Kapital[1], Kapital[2], Kapital[3]) + ",-";

            return returnString;
        }
        public string Difficulty(int index)
        {
            string returnString = string.Empty;

            if (index == 1)
            {
                returnString = ("Einfach");
            }
            else if (index == 2)
            {
                returnString = ("Mittel");
            }
            else if (index == 3)
            {
                returnString = ("Schwer");
            }

            return returnString;
        }
        public string SelectMonth(int index)
        {
            string returnString = string.Empty;

            if (index == 1)
            {
                returnString = ("Januar");
            }
            else if (index == 2)
            {
                returnString = ("Februar");
            }
            else if (index == 3)
            {
                returnString = ("März");
            }
            else if (index == 4)
            {
                returnString = ("April");
            }
            else if (index == 5)
            {
                returnString = ("Mai");
            }
            else if (index == 6)
            {
                returnString = ("Juni");
            }
            else if (index == 7)
            {
                returnString = ("Juli");
            }
            else if (index == 8)
            {
                returnString = ("August");
            }
            else if (index == 9)
            {
                returnString = ("September");
            }
            else if (index == 10)
            {
                returnString = ("Oktober");
            }
            else if (index == 11)
            {
                returnString = ("November");
            }
            else if (index == 12)
            {
                returnString = ("Dezember");
            }
            return returnString;
        }
        public string SelectCountry(int index)
        {
            string returnString = string.Empty;

            if (index == 7)
            {
                returnString = "Italien";
            }
            else if (index == 0)
            {
                returnString = "England";
            }
            else if (index == 2)
            {
                returnString = "Deutschland";
            }
            else if (index == 1)
            {
                returnString = "Frankreich";
            }
            else if (index == 6)
            {
                returnString = "Österreich";
            }


            return returnString;
        }
        public string hex_to_string_reverse_two_byte(int index)
        {
            string returnString = string.Empty;

            for (int i = index; i > index - 2; i--)
            {
                returnString += String.Format("{0,10:X}", configFile[i]); // BRINT DIE 2 BYTES IN DIE RICHTIGE REIHENFOLGE
            }

            returnString = returnString.Replace(" ", ""); // ERSETZT ALLE LEERZEICHEN IM STRING
            returnString = int.Parse(returnString, System.Globalization.NumberStyles.HexNumber).ToString(); // MACHT AUS DEN 2 BYTE EINEN INT UND DANACH EINEN STRING FÜR DIE AUSGABE

            return returnString;
        }
        private bool IsAnyTextBoxModified(Object sender , EventArgs e)
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                if (tb.Modified)
                {
                    return true;
                }
            }
            return false;
        }

        private void saveChanges(int position, int savelaenge, int TBnummer, bool YN, int booltrue)
        {

/*
foreach (Control x in this.Controls)
{
if (x is TextBox)
{
((TextBox)x).Text = String.Empty;
                    MessageBox.Show(x.ToString());  
}
}
*/


            //if (IsAnyTextBoxModified == true)
            //{
            //    MessageBox.Show(IsAnyTextBoxModified);
            //}
            //if (TBnummer == 1)
            //{

            //}
            /*
            if (textBox33.Modified == true)
            {
                // Länge des Namens schreiben
                configFile[position - 1] = Convert.ToByte(textBox33.TextLength);

                // Namen mit _ überschreiben (Ist original im Spiel auch so)

                int z = position;
                char[] yz = ("_______________".ToCharArray());
                foreach (byte b in yz)
                {
                    configFile[z] = (b); z++;
                }

                // Namen schreiben
                int y = 21;
                char[] xy = textBox33.Text.ToCharArray();
                foreach (byte b in xy)
                {
                    configFile[y] = (b); y++;
                }
                //SaveByteArrayToFileWithBinaryWriter(configFile, Program.SaveName);
                //MessageBox.Show("Änderungen in " + Program.DateiName + " gespeichert.");
            
                */
            //var bTextBox = (TextBox) this.Controls.Find("textBox"+ TBnummer.ToString(), true);
            // bTextBox = ("Textbox" + TBnummer.ToString() + ".Text");

            //bTextBox = "test";
            //MessageBox.Show(bTextBox.ToString());

            //(TBnummer).Text = "1";


            MessageBox.Show("Die Position an welche HEX der Eintrag geschriebne werden soll: " + position.ToString());
            MessageBox.Show("Die Länge des zu schreibenen Eintrages ist: " + savelaenge.ToString());
            MessageBox.Show("Die veränderte Textbox ist die: " + "TextBox" + TBnummer.ToString());
            MessageBox.Show("Soll die länge der TextBox mit angespeichert werden? " + YN.ToString());
            MessageBox.Show("Die Länge des Eintrags in der Textbox ist: " + booltrue.ToString());
            
            if (YN == true)
            {
                MessageBox.Show("Der Wert für die Länge des Inhalts wird an Position: " + (position - 1) + " gespeichert.");
            }

        }
    


        public void set_Text_data()
        {
            textBox41.Text = hex_to_string_reverse_two_byte(69);          // Anzahl Facharbeiter
            textBox1.Text = hex_to_string_reverse_two_byte(75) + ",-";    // Gehalt Facharbeiter
            textBox2.Text = hex_to_string_reverse_two_byte(77) + " Std."; // Wochenstunden Facharbeiter
            textBox25.Text = hex_to_string_reverse_two_byte(79);          //Anzahl Hilfsarbeiter
            textBox4.Text = hex_to_string_reverse_two_byte(85) + ",-";    //Gehalt Hilfsarbeiter
            textBox3.Text = hex_to_string_reverse_two_byte(87) + " Std."; //Wochenstunden Hilfsarbeiter
            textBox17.Text = hex_to_string_reverse_two_byte(89);          //Anzahl Ingenieure
            textBox6.Text = hex_to_string_reverse_two_byte(95) + ",-";    //Gehalt Ingenieure
            textBox5.Text = hex_to_string_reverse_two_byte(97) + " Std."; //Wochenstunden Ingenieure
            textBox42.Text = (configFile[9] + " " + SelectMonth(configFile[8]) + " " + hex_to_string_reverse_two_byte(7));
            textBox35.Text = Difficulty(configFile[10]);
            textBox33.Text = PlayerName();
            textBox34.Text = FirmName();
            textBox36.Text = SelectCountry(configFile[52]);  // Land auslesen (1Byte)
            textBox40.Text = Depts();
            textBox38.Text = (reverse_four_bytes(configFile[120], configFile[121], configFile[122], configFile[123]) + ",-"); // Personalkosten
            textBox39.Text = Earnings();
            textBox37.Text = capital();

        }
        public static void SaveByteArrayToFileWithBinaryWriter(byte[] data, string filePath)
        {
            using var writer = new BinaryWriter(File.OpenWrite(filePath));
            writer.Write(data);
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            configFile = Program.read_file_bytearray(); // LESE CONFIG FILE
            set_Text_data();
            this.Text = ("Dateien als Array auslesen - " + Program.DateiName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Für eine Funktion sind folgende Daten notwendig:
            // Int (Position an die die Textbox geschrieben werden muss) Beispiel: 20 für die länge des Namens und 21 für den Namen, Rest wird mit _ aufgefüllt
            // Int für die TextBox Nummer  --> geht nicht. Am besten alle Textboxen abspeichern.
            // Bool Abfrage, ob die Länge eines Eintrags gespeichert werden muss
            // int länge des Wertes, 0 Falls Bool false

             // Länge des Namens schreiben
             configFile[20] = Convert.ToByte(textBox33.TextLength);

             // Namen mit _ überschreiben (Ist original im Spiel auch so)

             int z = 21;
             char[] yz = ("_______________".ToCharArray());
             foreach (byte b in yz)
             {
                 configFile[z] = (b); z++;
             }

             // Namen schreiben
             int y = 21;
             char[] xy = textBox33.Text.ToCharArray();
             foreach (byte b in xy)
             {
                 configFile[y] = (b);y++;
             }
             SaveByteArrayToFileWithBinaryWriter(configFile, Program.SaveName);
             MessageBox.Show("Änderungen in " + Program.DateiName + " gespeichert.");

             //saveChanges(21, 16, 33, true, 9);
            

        }

    }
}
