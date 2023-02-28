using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public class InputValidator
    {
        public InputValidator()
        {
        }
        public string[] IsValidForAddingProduct(string input)
        {// -dw nazwa cena ilość, albo: //-dp nazwa cena ilość waga_lub_objętość jednostka(kg,l,x)
            string[] array = input.Split(" ");
            bool cnd1, cnd2, cnd3, cnd4;
            if (array.Length == 4 || array.Length == 6)
            {//produkt każdy
                cnd1 = float.TryParse(array[2], out float value1);//cena
                if (array.Length == 4)
                {//na wage
                    cnd2 = float.TryParse(array[3], out float value2);//waga
                    if(cnd1 && value1 >0 && cnd2 && value2>0)
                        return array;
                    else
                        throw new Exception("Invalid parameter format or value less or equal 0");
                }
                else //array ma 6
                {//na sztuki
                    cnd2 = int.TryParse(array[3], out int value2);//ilość sztuk
                    cnd3 = float.TryParse(array[4], out float value3);//waga lub pojemność sztuki
                    cnd4 = (array[5] == "kg" || array[5] == "l" || array[5] == "x");//jednostka
                    if (cnd1 && value1 > 0 && cnd2 && value2 > 0 && cnd3 &&  value3 > 0 && cnd4)
                        return array;
                    else
                        throw new Exception("Invalid parameter format or value less or equal 0");
                }     
            }
            else
            {
                throw new Exception("Invalid input for adding produkt");
            }
        }
        public string[] IsValidForUpdatingProduct(string input)
        {//-ac nazwa cena, albo: -aiw nazwa ilosc, albo: -aip nazwa ilosc
            string[] array = input.Split(" ");
            bool cnd1, cnd2, cnd3, cnd4;
            if (array.Length == 3)
            {
                cnd1 = float.TryParse(array[2], out float value1);//cena float zawsze
                cnd2 = float.TryParse(array[2], out float value2);
                cnd3 = int.TryParse(array[2], out int value3);
                if ((array[0] == "-ac") && cnd1 && value1 > 0 || (array[0] == "-aiw") && cnd2 && value2 > 0 || (array[0] == "-aip") && cnd3 && value3 > 0)
                    return array;
                else
                    throw new Exception("Error while parsing.");
            }
            else
            {
                throw new Exception("Invalid input for updating produkt");
            }
        }
    }
}
