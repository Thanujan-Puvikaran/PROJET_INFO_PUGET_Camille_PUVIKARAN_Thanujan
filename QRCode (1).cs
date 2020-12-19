using System;
using System.Text;

namespace PROJET_INFO_PUGET_Camille_PUVIKARAN_Thanujan
{
    class QRCode
    {
        // attribute(s)
        private string message;

        // data
        Pixel2 blanc = new Pixel2(255, 255, 255);
        Pixel2 noir = new Pixel2(0, 0, 0);
        Pixel2 bleu = new Pixel2(255, 0, 0);
        Pixel2 gris = new Pixel2(100, 100, 100);

        // constructor
        public QRCode(string message)
        {
            this.message = message;
            TestFonctionnement();
        }

        // properties        
        public string Message
        {
            get => this.message;
            set
            {
                message = value;
            }
        }

        // General method

        /// <summary>
        /// This method gathers all of the other methods.
        /// The methods it contains are used for each step of the creation of the QR Code.
        /// </summary>
        /// <returns>a matrix of pixel used to create the image of the QR Code</returns>
        public Pixel2[,] TestFonctionnement()                                                                   // FINIE
        {
            Pixel2[,] qrcodeblocs = RemplissageBlocs();                                 // 1ère étape : remplir les blocs qui ne bougent jamais           

            string messageConvert = TradMessage();                                      // 2ème étape : convertir le message de l'utilisateur en chaîne de bits (string)

            string message236and17 = BitMissing(messageConvert);                        // 3ème étape : créer un string avec les octets manquants pour aller à 152 bits (bits 236 et 17)

            string messageCorrection = Correction();                                    // 4ème étape : créer une chaîne contenant les bits de correction            

            string messageFinal = messageConvert + message236and17 + messageCorrection; // 5ème étape : créer une chaîne contenant les bits du message

            Pixel2[,] qrcodemessage = RemplissageMessage(qrcodeblocs, messageFinal);
            // 6ème étage : remplir le reste du QR Code (string des étapes 2/3/4)

            //Pixel2[,] qrcodeTaille4 = Redimentionnement(qrcodemessage);                 // 7ème étape : redimentionner la matrice pour en faire une image
            Pixel2[,] qrcodeTaille4 = Agrandissement(qrcodemessage, 16);                // 7ème étape : redimentionner la matrice pour en faire une image

            return qrcodeTaille4;

        } // fin méthode

        // Methods creating the QR Code

        /// <summary>
        /// This method creates the tree blocks at the corners of the QR Code.
        /// Theses blocks are always the same size, so this method can be used for any size of QR Code.
        /// It does not create the alignment pattern of versions 2 and up.
        /// </summary>
        /// <returns>a matrix of pixel with the tree blocks</returns>
        public Pixel2[,] RemplissageBlocs()                                                                     // FINIE
        {
            int longueur = 21;                                  // on fait comme si chaque QR code faisait 21x21
            int hauteur = 21;

            Pixel2[,] qrcode = new Pixel2[hauteur, longueur];

            for (int i = 0; i < 7; i++)                         // carré en haut à gauche
            {
                for (int j = 0; j < 7; j++)
                {
                    qrcode[i, j] = noir;
                }
            }
            for (int i = 0; i < 7; i++)                         // carré en haut à droite
            {
                for (int j = longueur - 7; j < longueur; j++)
                {
                    qrcode[i, j] = noir;
                }
            }
            for (int i = hauteur - 7; i < hauteur; i++)         // carré en bas à gauche
            {
                for (int j = 0; j < 7; j++)
                {
                    qrcode[i, j] = noir;
                }
            }

            for (int i = 1; i < 6; i++)                         // carrés blancs
            {
                qrcode[i, 1] = blanc;
                qrcode[i, 5] = blanc;
                qrcode[i, longueur - 2] = blanc;
                qrcode[i, longueur - 6] = blanc;
            }
            for (int i = hauteur - 6; i < hauteur - 1; i++)
            {
                qrcode[i, 1] = blanc;
                qrcode[i, 5] = blanc;
            }

            for (int j = 1; j < 5; j++)
            {
                qrcode[1, j] = blanc;
                qrcode[5, j] = blanc;
                qrcode[hauteur - 2, j] = blanc;
                qrcode[hauteur - 6, j] = blanc;
            }
            for (int j = longueur - 6; j < longueur - 1; j++)
            {
                qrcode[1, j] = blanc;
                qrcode[5, j] = blanc;
            }

            for (int i = 0; i < 8; i++)                         // séparateurs
            {
                qrcode[i, 7] = blanc;
                qrcode[i, longueur - 8] = blanc;
            }
            for (int j = 0; j < 8; j++)
            {
                qrcode[7, j] = blanc;
                qrcode[hauteur - 8, j] = blanc;
            }
            for (int i = hauteur - 8; i < hauteur; i++)
            {
                qrcode[i, 7] = blanc;
            }
            for (int j = longueur - 8; j < longueur; j++)
            {
                qrcode[7, j] = blanc;
            }

            for (int a = 0; a < 9; a++)                         // masque
            {
                qrcode[a, 8] = bleu;
                qrcode[8, a] = bleu;
            }
            for (int b = longueur - 8; b < longueur; b++)
            {
                qrcode[b, 8] = bleu;
                qrcode[8, b] = bleu;
            }

            for (int j = 8; j < longueur - 8; j++)              // bandes entre les gros carrés
            {
                if (j % 2 == 0) qrcode[6, j] = noir;
                else qrcode[6, j] = blanc;
            }
            for (int i = 8; i < hauteur - 8; i++)
            {
                if (i % 2 == 0) qrcode[i, 6] = noir;
                else qrcode[i, 6] = blanc;
            }

            qrcode[hauteur - 8, 8] = noir;

            for (int i = 0; i < qrcode.GetLength(0); i++)       // on remplit le reste de la matrice
            {
                for (int j = 0; j < qrcode.GetLength(1); j++)
                {
                    if (qrcode[i, j] == null) qrcode[i, j] = gris;
                }
            }

            return qrcode;
        } // fin méthode

        /// <summary>
        /// This method takes the message written by the user and converts it into a concatenation of numbers,
        /// which are then converted into a string made of chains of 11 bytes.
        /// In the end, it adds bytes to make the final chain a multiple of 8.
        /// </summary>
        /// <returns>a string made of bytes (0 and 1)</returns>
        public string TradMessage()                                                                             // FINIE
        {
            int longueur;                                                               // on définit la longueur du tableau d'int (et de byte) comprenant le message
            bool pair;

            if (this.message.Length % 2 == 0)                                             // en cas de nb de caractères pair, on fait un tableau de taille moitié  
            {
                longueur = this.message.Length / 2;
                pair = true;
            }
            else                                                                        // en cas de nb de caractères impair, on fait un tableau de taille moitié + 1
            {
                longueur = this.message.Length / 2 + 1;
                pair = false;
            }

            int[] messageConvertInt = new int[longueur];

            for (int i = 0; i < this.message.Length; i = i + 2)                         // on passe d'une chaîne de caractères à un tableau d'entiers
            {
                if ((this.message.Length % 2 != 0) && (i == (this.message.Length - 1)))   // on est au dernier byte qui est tout seul (cas d'un message impair)
                {
                    messageConvertInt[i / 2] = TradLatinToDecimal(this.message[i]);     //messageConvertInt[i / 2] = Trad()*45^0;
                }

                else                                                                    // on est sur un bloc de deux bits
                {
                    int temp2 = TradLatinToDecimal(this.message[i]);
                    int temp3 = TradLatinToDecimal(this.message[i + 1]);                //messageConvertInt[i / 2] = temp2*45^1 + temp3*45^0;
                    messageConvertInt[i / 2] = temp2 * 45 + temp3;
                }
            }

            string messageConvertByte = "0010";                                         // on ajoute l'encodage de l'alphanumérique

            string tailleMessageByte = TradDecimalToBinary(this.message.Length);
            messageConvertByte += tailleMessageByte.Substring(2);                       // on ajoute l'encodage du nombre de caractère (sur 9 bits au lieu de 11)

            for (int i = 0; i < messageConvertInt.Length; i++)                          // on passe d'un tableau d'entiers à un string (qui sont des octets de 11 bits alignés)
            {
                if (i == messageConvertInt.Length - 1)                                  // dernier int du tableau : dernier couple de lettre
                {
                    if (pair == true)                                                   // pair est true : deux lettres dans le "int", chaîne de 11 bits
                    {
                        messageConvertByte += TradDecimalToBinary(messageConvertInt[i]);
                    }
                    else                                                                // pair est false : une lettre dans le "int", chaîne de 6 bits
                    {
                        messageConvertByte += TradDecimalToBinary(messageConvertInt[i]).Substring(5);
                        // on obtient une chaîne de 11 bits, les 5 premiers sont 0, on les vire
                    }
                }
                else                                                                    // n - 1 premiers int du tableau : deux lettre dans le "int", chaîne de 11 bits
                {
                    messageConvertByte += TradDecimalToBinary(messageConvertInt[i]);
                }
            }

            messageConvertByte += "0000";                                               // on rajoute la terminaison

            while (messageConvertByte.Length % 8 != 0)                                  // on doit rajouter des "0" pour obtenir un multiple de 8
            {
                messageConvertByte += "0";
            }

            return messageConvertByte;
        } // fin méthode

        /// <summary>
        /// This method takes the message converted into bytes, and judging its length,
        /// adds more byte so that the string is long enough to fill all of the QR Code.
        /// </summary>
        /// <param name="messageConvertByte">the user's message converted in a string of bytes</param>
        /// <returns>the message with the bytes added</returns>
        public string BitMissing(string messageConvertByte)                                                     // FINIE
        {
            // fonctionne pour la version 1
            int bitsMissing = 152 - messageConvertByte.Length;
            int bytesMissing = bitsMissing / 8;

            string message236et17 = "";

            for (int i = 0; i < bytesMissing; i++)
            {
                message236et17 += "11101100";   // octet de 236
                message236et17 += "00010001";   // octet de 17
            }

            return message236et17;
        } // fin méthode

        /// <summary>
        /// This method generates the bytes used for the correction of the QR Code.
        /// </summary>
        /// <returns>a string of the bytes we need to add to the QR Code</returns>
        public string Correction()                                                                              // FINIE
        {
            string correction = "";

            Encoding u8 = Encoding.UTF8;
            string a = "HELLO WORD";
            int iBC = u8.GetByteCount(a);
            byte[] bytesa = u8.GetBytes(a);
            string b = "HELLO WORF";
            byte[] bytesb = u8.GetBytes(b);
            byte[] result = ReedSolomonAlgorithm.Encode(bytesa, 7, ErrorCorrectionCodeType.QRCode);

            for (int i = 0; i < result.Length; i++)
            {
                correction += Convert.ToString(TradDecimalToBinary(result[i]));
            }

            return correction;
        } // fin méthode

        /// <summary>
        /// This method fills the matrix of pixels with the message (the string of bytes).
        /// </summary>
        /// <param name="qrcode">matrix of pixel (with only the blocks)</param>
        /// <param name="messageFinal">the user's message converted into a string of bytes</param>
        /// <param name="messageCorrection">the correction needed</param>
        /// <returns>the matrix with the final message in it, ready to become a picture</returns>
        public Pixel2[,] RemplissageMessage(Pixel2[,] qrcode, string messageFinal)                              // FINIE
        {
            int longueur = qrcode.GetLength(0);                 // pour plus de lisibilité (et parce qu'un QR Code est toujours carré)
            int cmpt = 0;

            for (int f = longueur - 1; f >= 1; f -= 2)
            {
                if (f % 4 == 0)                                 // on va de bas en haut (on monte)
                {
                    for (int d = longueur - 1; d >= 0; d--)
                    {
                        if (qrcode[d, f] == gris)
                        {
                            if (messageFinal[cmpt] == '1') qrcode[d, f] = noir;
                            if (messageFinal[cmpt] == '0') qrcode[d, f] = blanc;
                            cmpt++;
                        }
                        if (qrcode[d, f - 1] == gris)
                        {
                            if (messageFinal[cmpt] == '1') qrcode[d, f - 1] = noir;
                            if (messageFinal[cmpt] == '0') qrcode[d, f - 1] = blanc;
                            cmpt++;
                        }
                    }
                }
                if ((f % 4 != 0) && (f % 2 == 0))               // on va de haut en bas (on descend)
                {
                    for (int e = 0; e < longueur; e++)
                    {
                        if (qrcode[e, f] == gris)
                        {
                            if (messageFinal[cmpt] == '1') qrcode[e, f] = noir;
                            if (messageFinal[cmpt] == '0') qrcode[e, f] = blanc;
                            cmpt++;
                        }
                        if (qrcode[e, f - 1] == gris)
                        {
                            if (messageFinal[cmpt] == '1') qrcode[e, f - 1] = noir;
                            if (messageFinal[cmpt] == '0') qrcode[e, f - 1] = blanc;
                            cmpt++;
                        }
                    }
                }
            }

            for (int k = longueur - 1; k >= 0; k--)             // on va de bas en haut (première colonne)
            {
                if (qrcode[k, 0] == gris)
                {
                    if (messageFinal[cmpt] == '1') qrcode[k, 0] = noir;
                    if (messageFinal[cmpt] == '0') qrcode[k, 0] = blanc;
                    cmpt++;
                }
            }

            string masque = "111011111000100";

            int cmptMasqueV = 0;                                // compteur pour la colonne (vertical)
            int cmptMasqueH = 0;                                // compteur pour la ligne (horizontal)

            for (int s = longueur - 1; s >= 0; s--)             // on remplit le masque (colonne)
            {
                if (qrcode[s, 8] == bleu)
                {
                    if (masque[cmptMasqueV] == '1') qrcode[s, 8] = noir;
                    if (masque[cmptMasqueV] == '0') qrcode[s, 8] = blanc;

                    cmptMasqueV++;
                }
            }

            for (int t = 0; t < longueur; t++)                  // on remplit le masque (ligne)
            {
                if (qrcode[8, t] == bleu)
                {
                    if (masque[cmptMasqueH] == '1') qrcode[8, t] = noir;
                    if (masque[cmptMasqueH] == '0') qrcode[8, t] = blanc;

                    cmptMasqueH++;
                }
            }

            return qrcode;
        } // fin méthode

        /// <summary>
        /// The program doesn't work when the width/length of the image are not a multiple of 4.
        /// This method adds white pixels to the image so that it can be turned into a picture.
        /// </summary>
        /// <param name="qrcode">the matrix of pixels in the original size</param>
        /// <returns>the matrix of pixel with its width/length being a multiple of 4</returns>
        public Pixel2[,] Redimentionnement(Pixel2[,] qrcode)                                                    // FINIE
        {
            int longueur = qrcode.GetLength(0);

            while (longueur % 4 != 0)
            {
                longueur++;
            }

            Pixel2[,] qrcodebonnetaille = new Pixel2[longueur, longueur];

            for (int i = 0; i < qrcode.GetLength(0); i++)
            {
                for (int j = 0; j < qrcode.GetLength(0); j++)
                {
                    qrcodebonnetaille[i, j] = qrcode[i, j];
                }
            }

            for (int s = 0; s < longueur; s++)
            {
                for (int t = 0; t < longueur; t++)
                {
                    if (qrcodebonnetaille[s, t] == null) qrcodebonnetaille[s, t] = blanc;
                }
            }

            return qrcodebonnetaille;
        } // fin méthode

        /// <summary>
        /// This method enlarges the matrix of pixels so that the image is bigger.
        /// We use it in the QR Code to make the size of the image a multiple of 4.
        /// </summary>
        /// <param name="qrcode"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public Pixel2[,] Agrandissement(Pixel2[,] qrcode, int zoom)                                             // FINIE
        {
            int taille = qrcode.GetLength(0);

            Pixel2[,] qrcodeGrande = new Pixel2[taille * zoom, taille * zoom];

            int tailleGrande = qrcodeGrande.GetLength(0);

            for (int i = 0; i < tailleGrande; i += zoom)
            {
                for (int j = 0; j < tailleGrande; j += zoom)
                {

                    for (int a = 0; a < zoom; a++)
                    {
                        for (int b = 0; b < zoom; b++)
                        {
                            qrcodeGrande[i + a, j + b] = qrcode[i / zoom, j / zoom];
                        }
                    }
                }
            }

            return qrcodeGrande;
        } // fin méthode

        // Other methods

        /// <summary>
        /// This method converts a letter/number/symbol into a integer (following the rule of the alphanumeric alphabet).
        /// </summary>
        /// <param name="message">a char of the message's letter</param>
        /// <returns>the letter/number/symbol converted</returns>
        public int TradLatinToDecimal(char message)                                                             // FINIE
        {
            int rep;

            if (message == '0') rep = 0;
            else if (message == '1') rep = 1;
            else if (message == '2') rep = 2;
            else if (message == '3') rep = 3;
            else if (message == '4') rep = 4;
            else if (message == '5') rep = 5;
            else if (message == '6') rep = 6;
            else if (message == '7') rep = 7;
            else if (message == '8') rep = 8;
            else if (message == '9') rep = 9;
            else if ((message == 'A') || (message == 'a')) rep = 10;
            else if ((message == 'B') || (message == 'b')) rep = 11;
            else if ((message == 'C') || (message == 'c')) rep = 12;
            else if ((message == 'D') || (message == 'd')) rep = 13;
            else if ((message == 'E') || (message == 'e')) rep = 14;
            else if ((message == 'F') || (message == 'f')) rep = 15;
            else if ((message == 'G') || (message == 'g')) rep = 16;
            else if ((message == 'H') || (message == 'h')) rep = 17;
            else if ((message == 'I') || (message == 'i')) rep = 18;
            else if ((message == 'J') || (message == 'j')) rep = 19;
            else if ((message == 'K') || (message == 'k')) rep = 20;
            else if ((message == 'L') || (message == 'l')) rep = 21;
            else if ((message == 'M') || (message == 'm')) rep = 22;
            else if ((message == 'N') || (message == 'n')) rep = 23;
            else if ((message == 'O') || (message == 'o')) rep = 24;
            else if ((message == 'P') || (message == 'p')) rep = 25;
            else if ((message == 'Q') || (message == 'q')) rep = 26;
            else if ((message == 'R') || (message == 'r')) rep = 27;
            else if ((message == 'S') || (message == 's')) rep = 28;
            else if ((message == 'T') || (message == 't')) rep = 29;
            else if ((message == 'U') || (message == 'u')) rep = 30;
            else if ((message == 'V') || (message == 'v')) rep = 31;
            else if ((message == 'W') || (message == 'w')) rep = 32;
            else if ((message == 'X') || (message == 'x')) rep = 33;
            else if ((message == 'Y') || (message == 'y')) rep = 34;
            else if ((message == 'Z') || (message == 'z')) rep = 35;
            else if (message == ' ') rep = 36;
            else if (message == '$') rep = 37;
            else if (message == '%') rep = 38;
            else if (message == '*') rep = 39;
            else if (message == '+') rep = 40;
            else if (message == '-') rep = 41;
            else if (message == '.') rep = 42;
            else if (message == '/') rep = 43;
            else if (message == ':') rep = 44;

            else rep = 100;

            return rep;
        } // fin méthode

        /// <summary>
        /// This method takes an integer and converts it into a string of 11 bytes.
        /// </summary>
        /// <param name="nb">the number we want to convert</param>
        /// <returns>a string of bytes</returns>
        public string TradDecimalToBinary(int nb)                                                               // FINIE
        {
            string rep = "";
            // Math.Pow(x, a) = x^a = x à la puissance a

            for (int i = 10; i >= 0; i--)                   // on prend en argument un nombre entier décimal (positif, inférieur strict à 2048)
            {
                if ((nb - Math.Pow(2, i)) < 0)              // le nombre est plus petit que la puissance de 2, on ajoute "0" au string
                {
                    rep += "0";
                }
                if ((nb - Math.Pow(2, i)) >= 0)             // le nombre est plus grand que la puissance de 2, on ajoute "1" au string et on soustrait la puissance au nombre
                {
                    rep += "1";
                    nb = nb - Convert.ToInt32(Math.Pow(2, i));
                }
            }

            return rep;
        } // fin méthode
    }
}
