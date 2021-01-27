using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            string alphabetic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] letters = new char[alphabetic.Length];
            letters = alphabetic.ToCharArray();

            char[] plaintext;
            char[] ciphertext;
            char[] keystream;
            char[] anlyzingkey;
            int[] textarr;
            int[] cipherarr;
            int[] resultarr;
            int[] keystreamarr;
            string k, c, finalkey = "", finalfinal = "";
            int counter = 0;


            c = plainText;
            plaintext = new char[c.Length];
            plaintext = c.ToCharArray();

            k = cipherText;
            ciphertext = new char[k.Length];
            ciphertext = k.ToCharArray();


            cipherarr = new int[ciphertext.Length];
            resultarr = new int[plaintext.Length];
            keystream = new char[plaintext.Length];
            keystreamarr = new int[plaintext.Length];
            textarr = new int[plaintext.Length];
            anlyzingkey = new char[plaintext.Length];


            //to get the indexes of the plain text
            for (int i = 0; i < plaintext.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (plaintext[i] == ' ')
                    {
                        textarr[i] = -1;
                        continue;
                    }

                    if (plaintext[i] == letters[j])
                    {
                        textarr[i] = j;
                        break;
                    }
                }
            }


            //to get the indexes of the cipher text
            for (int i = 0; i < ciphertext.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (ciphertext[i] == ' ')
                    {
                        cipherarr[i] = -1;
                        continue;
                    }

                    if (ciphertext[i] == letters[j])
                    {
                        cipherarr[i] = j;
                        break;
                    }
                }
            }


            //Analyze
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (cipherarr[i] < textarr[i])
                    resultarr[i] = (cipherarr[i] - textarr[i]) + 26;
                else
                    resultarr[i] = cipherarr[i] - textarr[i];
            }



            //to get the text of the Analyzing key
            int r = 0;
            for (int l = 0; l < resultarr.Length; l++)
            {
                r = resultarr[l];
                if (r == -1)
                {
                    anlyzingkey[l] = ' ';
                }

                else
                {
                    anlyzingkey[l] = letters[r];
                }
            }
            string analyze = new string(anlyzingkey);
            Console.WriteLine(analyze.ToLower());


            int w = 0;


            for (int i = 0; i < plaintext.Length; i++)
            {
                finalkey += anlyzingkey[i];
                counter++;
                if (i != 0)
                {
                    if ((analyze[w] == analyze[i]) || ((analyze[w].ToString().ToUpper() == anlyzingkey[i].ToString().ToUpper())))
                    {
                        if ((analyze[w + 1] == analyze[i + 1]) || ((analyze[w + 1].ToString().ToUpper() == anlyzingkey[i + 1].ToString().ToUpper())))
                        {
                            if ((anlyzingkey[w + 2] == anlyzingkey[i + 2]) || (analyze[w + 2].ToString().ToUpper() == anlyzingkey[i + 2].ToString().ToUpper()))
                            {
                                counter--;
                                finalfinal = finalkey.Remove(counter);
                                break;
                            }
                        }
                    }
                }

            }


            return (finalfinal.ToLower());

        }

        public string Decrypt(string cipherText, string key)
        {
            string alphabetic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] letters = new char[alphabetic.Length];
            letters = alphabetic.ToCharArray();

            char[] ciphertext;
            char[] keyy;
            char[] keystream;
            char[] decrypttext;
            int[] cipherarr;
            int[] keyarr;
            int[] resultarr;
            int[] keystreamarr;
            string k, c;

            c = cipherText.ToLower();
            ciphertext = new char[c.Length];
            ciphertext = c.ToCharArray();

            k = key;
            keyy = new char[k.Length];
            keyy = k.ToCharArray();


            keyarr = new int[keyy.Length];
            resultarr = new int[ciphertext.Length];
            keystream = new char[ciphertext.Length];
            keystreamarr = new int[ciphertext.Length];
            cipherarr = new int[ciphertext.Length];
            decrypttext = new char[ciphertext.Length];


            //to get the indexes of the cipher text
            for (int i = 0; i < ciphertext.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (ciphertext[i] == ' ')
                    {
                        cipherarr[i] = ' ';
                        continue;
                    }

                    if (ciphertext[i] == letters[j])
                    {
                        cipherarr[i] = j;
                        break;
                    }
                }
            }


            //to get the indexes of the key
            for (int i = 0; i < keyy.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (keyy[i] == ' ')
                    {
                        keyarr[i] = -1;
                        continue;
                    }

                    if (keyy[i] == letters[j])
                    {
                        keyarr[i] = j;
                        break;
                    }
                }
            }


            //to get the key stream
            int u = 0;
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (ciphertext[i] == ' ')
                {
                    keystream[i] = ' ';
                    continue;
                }

                if (u == keyy.Length)
                    u = 0;

                keystream[i] = Convert.ToChar(keyy[u]);
                u++;
            }



            //to get the indexes of the keyStream
            for (int i = 0; i < keystream.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (keystream[i] == ' ')
                    {
                        keystreamarr[i] = ' ';
                        continue;
                    }

                    if (keystream[i] == letters[j])
                    {
                        keystreamarr[i] = j;
                        break;
                    }
                }
            }


            //decryption
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if ((cipherarr[i] == ' ') || (keystreamarr[i] == ' '))
                {
                    resultarr[i] = -1;
                    continue;
                }
                if (cipherarr[i] < keystreamarr[i])
                    resultarr[i] = ((cipherarr[i] - keystreamarr[i]) + 26);

                else
                    resultarr[i] = (cipherarr[i] - keystreamarr[i]);
            }


            //to get the text of the decryption
            int r = 0;
            for (int l = 0; l < resultarr.Length; l++)
            {
                r = resultarr[l];
                if (r == -1)
                {
                    decrypttext[l] = ' ';
                }
                else
                {
                    decrypttext[l] = letters[r];
                }
            }
            string decrypt = new string(decrypttext);
            return decrypt;
        }

        public string Encrypt(string plainText, string key)
        {
            string alphabetic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] letters = new char[alphabetic.Length];
            letters = alphabetic.ToCharArray();

            char[] plaintext;
            char[] ciphertext;
            char[] Key;
            char[] keystream;
            int[] textarr;
            int[] keyarr;
            int[] resultarr;
            int[] keystreamarr;
            string p, k;


            //Console.WriteLine("Please enter the plain text: ");
            p = plainText;
            plaintext = new char[p.Length];
            plaintext = p.ToCharArray();


            // Console.WriteLine("Please enter the key: ");
            k = key;
            Key = new char[k.Length];
            Key = k.ToCharArray();

            textarr = new int[plaintext.Length];
            keyarr = new int[Key.Length];
            resultarr = new int[plaintext.Length];
            keystream = new char[plaintext.Length];
            keystreamarr = new int[plaintext.Length];
            ciphertext = new char[plaintext.Length];


            //to get the indexes of the plain text
            for (int i = 0; i < plaintext.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (plaintext[i] == ' ')
                    {
                        textarr[i] = ' ';
                        continue;
                    }

                    if (plaintext[i] == letters[j])
                    {
                        textarr[i] = j;
                        break;
                    }
                }
            }



            //to get the indexes of the key
            for (int i = 0; i < Key.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (Key[i] == ' ')
                    {
                        keyarr[i] = ' ';
                        continue;
                    }

                    if (Key[i] == letters[j])
                    {
                        keyarr[i] = j;
                        break;
                    }
                }
            }


            //to get the key stream
            int u = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (plaintext[i] == ' ')
                {
                    keystream[i] = ' ';
                    continue;
                }

                if (u == Key.Length)
                    u = 0;

                keystream[i] = Convert.ToChar(Key[u]);
                u++;
            }


            //to get the indexes of the keyStream
            for (int i = 0; i < keystream.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (keystream[i] == ' ')
                    {
                        keystreamarr[i] = ' ';
                        continue;
                    }

                    if (keystream[i] == letters[j])
                    {
                        keystreamarr[i] = j;
                        break;
                    }
                }
            }


            //The encryption
            for (int i = 0; i < plaintext.Length; i++)
            {
                resultarr[i] = (textarr[i] + keystreamarr[i]) % 26;
            }

            //to get the text of the encryption
            int s = 0;
            for (int l = 0; l < resultarr.Length; l++)
            {
                s = resultarr[l];
                if (s == 32)
                    ciphertext[l] = ' ';
                else
                    ciphertext[l] = letters[s];
            }

            string final = new string(ciphertext);

            return final.ToUpper();
        }
    }
}
