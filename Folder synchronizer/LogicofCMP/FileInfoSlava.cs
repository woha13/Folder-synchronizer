using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LogicofCMP
{
    public partial class ListsofFiles
    {
        // applying show options
        public void deleteAllDelete(bool checkboxRightIsChecked, bool CheckboxLeftIsChecked, bool checkBoxEqualIsChecked, bool checkBoxNotEqualIsChecked)
        {
            for (int ii = 0; ii<listWohaAllConnected.Count; ii++)
            {
                int jj = listWohaAllConnected.ElementAt(ii).Relations;

                //removing from view all items with Delete relations
                if ((jj==LinksInfo.DeleteIcon || jj == LinksInfo.NotEqualIcon) && checkBoxNotEqualIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }

                //removing from view all items with copy to right relations
                if ((jj == LinksInfo.RightIcon || jj == LinksInfo.NotEqualToRight) && checkboxRightIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }

                //removing from view all items with copy to left relations
                if ((jj == LinksInfo.LeftIcon || jj == LinksInfo.NotEqualToLeft) && CheckboxLeftIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }

                //removing from view all items with equal relations
                if (jj == LinksInfo.EqualIcon && checkBoxEqualIsChecked == false)
                {
                    listWohaAllConnected.RemoveAt(ii);
                    ii--;
                }
            }
        }
    }
}
