using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
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


            //to get the text of the Analyzing key (keystream)
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


            //to get only the key
            int w = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                finalkey += anlyzingkey[i];
                counter++;
                if (i != 0)
                {
                    if ((plaintext[w] == analyze[i]) || ((plaintext[w].ToString().ToUpper() == anlyzingkey[i].ToString().ToUpper())))
                    {
                        if ((plaintext[w + 1] == analyze[i + 1]) || ((plaintext[w + 1].ToString().ToUpper() == anlyzingkey[i + 1].ToString().ToUpper())))
                        {
                            if ((plaintext[w + 2] == anlyzingkey[i + 2]) || (plaintext[w + 2].ToString().ToUpper() == anlyzingkey[i + 2].ToString().ToUpper()))
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
            int[] cipherarr;
            int[] keyarr;
            int[] resultarr;
            int[] keystreamarr;
            int[] decrypttextarr;
            string k, c;
            string keystream = "";
            string decrypttext = "";
            int counter = -1;


            c = cipherText;
            ciphertext = new char[c.Length];
            ciphertext = c.ToCharArray();


            k = key;
            keyy = new char[k.Length];
            keyy = k.ToCharArray();


            keyarr = new int[ciphertext.Length];
            resultarr = new int[ciphertext.Length];
            keystreamarr = new int[ciphertext.Length];
            cipherarr = new int[ciphertext.Length];
            decrypttextarr = new int[cipherarr.Length];


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

                    else if (ciphertext[i] == letters[j])
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
                        keystreamarr[i] = -1;
                        counter++;
                        continue;
                    }

                    else if (keyy[i] == letters[j])
                    {
                        keyarr[i] = j;
                        keystreamarr[i] = j;
                        counter++;
                        break;
                    }
                }
            }


            //for decryption
            counter++;
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (counter < ciphertext.Length)
                {
                    if (cipherarr[i] < keystreamarr[i])
                    {
                        resultarr[i] = (cipherarr[i] - keystreamarr[i]) + 26;
                        keystreamarr[counter] = resultarr[i];
                        counter++;
                    }
                    else
                    {
                        resultarr[i] = cipherarr[i] - keystreamarr[i];
                        keystreamarr[counter] = resultarr[i];
                        counter++;
                    }
                }
                else
                {
                    if (cipherarr[i] < keystreamarr[i])
                    {
                        resultarr[i] = (cipherarr[i] - keystreamarr[i]) + 26;
                        //keystreamarr[i] = resultarr[i];
                    }
                    else
                    {
                        resultarr[i] = cipherarr[i] - keystreamarr[i];
                        //keystreamarr[i] = resultarr[i];
                    }
                }

            }

            for (int i = 0; i < ciphertext.Length; i++)
            {
                if ((cipherarr[i] == -1) || (keystreamarr[i] == -1))
                    decrypttextarr[i] = -1;

                else if (cipherarr[i] < keystreamarr[i])
                    decrypttextarr[i] = (cipherarr[i] - keystreamarr[i]) + 26;
                else
                    decrypttextarr[i] = cipherarr[i] - keystreamarr[i];
            }

            //to get the text of the keystream
            int s = 0;
            for (int l = 0; l < keystreamarr.Length; l++)
            {
                s = keystreamarr[l];
                if (s == -1)
                    keystream += ' ';
                else
                    keystream += letters[s];
            }



            //to get the text of the decryption
            int ss = 0;
            for (int l = 0; l < decrypttextarr.Length; l++)
            {
                ss = decrypttextarr[l];
                if (ss == -1)
                    decrypttext += ' ';
                else
                    decrypttext += letters[ss];
            }

            // string final = new string(keystream);
            return (decrypttext.ToLower());
        }

        public string Encrypt(string plainText, string key)
        {
            string alphabetic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] letters = new char[alphabetic.Length];
            letters = alphabetic.ToCharArray();

            char[] plaintext;
            char[] keyy;
            char[] keystream;
            char[] cipher;
            int[] textarr;
            int[] keyyarr;
            int[] resultarr;
            int[] keystreamarr;
            string k, c;

            c = plainText;
            plaintext = new char[c.Length];
            plaintext = c.ToCharArray();


            k = key;
            keyy = new char[k.Length];
            keyy = k.ToCharArray();


            keyyarr = new int[keyy.Length];
            resultarr = new int[plaintext.Length];
            keystream = new char[plaintext.Length];
            keystreamarr = new int[plaintext.Length];
            textarr = new int[plaintext.Length];
            cipher = new char[plaintext.Length];


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


            //to get the indexes of the keyy text
            for (int i = 0; i < keyy.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (keyy[i] == ' ')
                    {
                        keyyarr[i] = -1;
                        continue;
                    }

                    if (keyy[i] == letters[j])
                    {
                        keyyarr[i] = j;
                        break;
                    }
                }
            }

            //to get the key stream
            int u = 0, d = 0;
            for (int i = 0; i < plaintext.Length; i++)
            {
                if (plaintext[i] == ' ')
                {
                    keystream[i] = ' ';
                    continue;
                }

                if (u >= keyy.Length)
                {
                    keystream[i] = Convert.ToChar(plaintext[d]);
                    d++;
                }

                else
                {
                    keystream[i] = Convert.ToChar(keyy[u]);
                    u++;
                }
            }

            //to get the indexes of the keyStream
            for (int i = 0; i < keystream.Length; i++)
            {
                for (int j = 0; j < letters.Length; j++)
                {
                    if (keystream[i] == ' ')
                    {
                        keystreamarr[i] = -1;
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
                if (s == -1)
                    cipher[l] = ' ';
                else
                    cipher[l] = letters[s];
            }

            string final = new string(cipher);
            return (final).ToUpper();
        }
    }
}
