using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            //  Console.WriteLine(plainText.Length);
            List<int> key = new List<int>();

            if (plainText.Contains(" "))
            {
                plainText = plainText.Replace(" ", "");
            }

            int lentext = plainText.Length;

            int index = 0;
            for (int i = 0; i < plainText.Length; i++)
            {


                if (plainText[i] == cipherText[index])
                    index++;

            }

            //double check
            int oldindex = index;

            Console.WriteLine("old index: " + oldindex);
            Console.WriteLine("2nd char: " + cipherText[index]);
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == cipherText[index])
                    index++;
            }
            Console.WriteLine("index : " + index);

            int rows = Math.Min(oldindex, index - oldindex + 1);
            int cols = 0;
            int count = lentext;
            Console.WriteLine("da al rows :  " + rows);
            while (count > rows)
            {
                count -= rows;
                cols++;
            }
            cols++;

            int xnum = rows - count;

            for (int i = 0; i < xnum; i++)
            {
                plainText += ".";
            }
            // Console.WriteLine(cipherText);

            //Console.WriteLine("cols" + cols);
            //Console.WriteLine("rows" + rows);
            //Console.WriteLine("cipher:   " + cipherText);
            //Console.WriteLine("plain:  " + plainText);
            index = 0;
            char[,] array = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = plainText[index];
                    index++;
                }
            }

            //for (int i = 0; i < rows; i++)
            //{
            //    for (int j = 0; j < cols; j++)
            //    {
            //        Console.Write(array[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            List<int> dots = new List<int>();


            for (int i = 0; i < cols; i++)
            {
                if (array[rows - 1, i] == '.')
                {
                    dots.Add(i);
                    //  Console.WriteLine(i);
                }
            }

            //List<char> xcolumn = new List<char>(); 
            string xcoloumn = "";
            for (int i = 0; i < dots.Count; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    xcoloumn += array[j, dots[i]];
                }
            }

            //  Console.WriteLine(xcoloumn);

            string[] colsplit = new string[dots.Count];

            colsplit = xcoloumn.Split('.');


            //for (int i = 0; i < colsplit.Count(); i++)
            //{
            //   Console.WriteLine (colsplit[i]);
            //}
            //index = 0;
            //int c = 0;
            //int jj = 0;
            //for (int i = 0; i < cipherText.Length; i++)
            //{
            //    if (cipherText[i].Equals(colsplit[index]))
            //    {
            //        c++;
            //        index++;
            //    }
            //    if (c == rows)
            //    {
            //        cipherText = cipherText.Insert(i, ".");
            //        c = 0;
            //    }
            //}

            // List<int> slot=new List<int>();
            for (int i = 0; i < dots.Count; i++)
            {
                //   slot.Add(cipherText.IndexOf(colsplit[i]));
                int slot = cipherText.IndexOf(colsplit[i]) + rows - 1;
                cipherText = cipherText.Insert(slot, ".");
                // Console.WriteLine("ssssssss: "+cipherText.IndexOf(colsplit[i]));
            }


            //Console.WriteLine(" ----->>", cipherText);
            List<int> arr = new List<int>();
            index = 0;
            while (arr.Count != cols)
            {
                for (int i = 0; i < cols; i++)
                {
                    int s = 0;
                    if (arr.Count == cols)
                    {
                        break;
                    }
                    for (int j = 0; j < rows; j++)
                    {
                        if (array[j, i] == cipherText[index])
                        {
                            s++;
                            index++;
                        }
                        else
                            break;
                    }

                    if (s == rows)
                    {
                        arr.Add(i);
                    }
                    else
                    {
                        index -= s;
                    }
                }

            }



            int[] a = new int[arr.Count];
            for (int i = 0; i < arr.Count; i++)
            {
                a[arr[i]] = i;
            }

            for (int i = 0; i < a.Length; i++)
            {
                key.Add(a[i]);
            }
            for (int i = 0; i < cols; i++)
            {
                key[i] += 1;
            }


            return key;


        }

        public string Decrypt(string cipherText, List<int> key)
        {
            string pt = "";

            int lenkey = key.Count;
            int lentext = cipherText.Length;

            int rows = lentext / lenkey;

            char[,] array = new char[rows, lenkey];

            int count = 0;

            List<int> keydumy = new List<int>();
            keydumy = key.ToList();

            for (int i = 0; i < lenkey; i++)
            {
                keydumy[i] -= 1;
            }

            for (int j = 0; j < lenkey; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    array[i, j] = cipherText[count];
                    count++;
                }
            }

            char[,] f = new char[rows, lenkey];

            for (int i = 0; i < lenkey; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    f[j, keydumy.IndexOf(i)] = array[j, i];
                }
            }

            // Console.WriteLine();

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < lenkey; i++)
                {
                    //Console.Write(f[j, i]);
                    pt += f[j, i];

                }
                // Console.WriteLine();
            }




            return pt;
        }

        public string Encrypt(string plainText, List<int> key)
        {
            string cp = "";

            if (plainText.Contains(" "))
            {
                plainText = plainText.Replace(" ", "");
            }

            int lenkey = key.Count();
            int lentext = plainText.Length;
            int added = 0;

            List<int> keydumy = key.ToList();

            for (int i = 0; i < lenkey; i++)
            {
                keydumy[i] -= 1;

                // Console.WriteLine("da hna al encrypt: "+keydumy[i]);
            }


            if (lentext % lenkey != 0)
            {
                while (lentext % lenkey != 0)
                {

                    lentext++;
                    added++;

                }
            }


            for (int i = 0; i < added; i++)
            {
                plainText += "x";
            }


            int rows = lentext / lenkey;

            char[,] array = new char[rows, lenkey];

            int count = 0;


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < lenkey; j++)
                {
                    array[i, j] = plainText[count];

                    count++;
                }
            }

            List<int> numbers = new List<int>();
            for (int i = 0; i < lenkey; i++)
            {
                numbers.Add(keydumy.IndexOf(i));
                //    Console.WriteLine(numbers[i]);
            }

            for (int i = 0; i < lenkey; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    cp += array[j, numbers[i]];
                }
            }


            return cp;
        }
    }
}
