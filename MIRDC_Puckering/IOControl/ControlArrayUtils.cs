using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace controlArray
{
    class ControlArrayUtils
    {        
        public static ArrayList  getControlArray(System.Windows.Forms.Control frm, string controlName,string separator)
        {
            //short i;
            short startOfIndex;
           
            ArrayList alist = new ArrayList();      
            string strSuffix;
            short maxIndex = -1;
            foreach (Control EnumControl in frm.Controls )

            {
               
                startOfIndex = Convert.ToInt16 (EnumControl.Name.IndexOf(controlName.ToLower() + separator));
                if (startOfIndex == 0)
                {
                    strSuffix =EnumControl.Name.Substring(controlName.Length);
                    if (IsInteger(strSuffix))
                    {
                        if (Convert.ToInt16 (strSuffix) > maxIndex)
                        {
                            maxIndex = Convert.ToInt16(strSuffix);
                        }
                    }
                }
            }
            if (maxIndex > -1)
            {
                for ( short  j = 0; j  <= maxIndex; j ++)
                {
                    System.Windows .Forms.Control aControl = getControlFromName(frm, controlName, j ,separator);
                    if (!((aControl == null)))
                    {
                          System.Type controlType = aControl.GetType()  ;
                          
                    }

                    alist.Add(aControl);
                }
            }
            return alist;
        }

        private static System.Windows.Forms.Control getControlFromName(System .Windows.Forms.Control  frm, string controlName, short index,String separator)
        {
            controlName = controlName + separator + index;
            foreach (Control EnumControl in frm.Controls)
            {
                if (string.Compare(EnumControl.Name, controlName, true) == 0)
                {
                    return EnumControl;
                }
            }
            return null;
        }

        private static bool IsInteger(string Value)
        {
            if (Value == "")
            {
                return false;
            }
            foreach (char chr in Value)
            {
                if (!(char.IsDigit(chr)))
                {
                    return false;
                }
            }
            return true;
        }
    }
}


