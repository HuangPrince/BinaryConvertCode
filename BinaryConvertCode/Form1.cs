using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryConvertCode
{
    public partial class Form1 : Form
    {
        private DataGridView dataGridView;
        private Image image;
        private Random random = new Random();
        private List<int> listInt;
        private int[] CodeVector;
        public Form1()
        {
            InitializeComponent();
            //获取图片            
            string strPath = Application.StartupPath + "\\Source\\circel.JPG";
            image = Image.FromFile(strPath);

            listInt = new List<int>();
            textBox_ShowCode.Font = new Font(textBox_ShowCode.Font.FontFamily, 16);

            dataGridView = new DataGridView(); ;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.ColumnCount = 11;
            dataGridView.RowCount = 2;
            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.CellFormatting += DataGridView_CellFormatting1;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    int iRandom = random.Next(0, 100);
                    if (iRandom % 2 == 0)
                    {
                        listInt.Add(1);
                        DataGridViewImageCell cell = new DataGridViewImageCell();
                        cell.Value = image;
                        dataGridView.Rows[i].Cells[j] = cell;
                    }
                    else
                    {
                        listInt.Add(0);
                    }
                }
            }
            this.Controls.Add(dataGridView);
        }

        /// <summary>
        /// 调整图片以适应单元格大小
        /// </summary>
        private void DataGridView_CellFormatting1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is Image && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Image image = (Image)e.Value;
                int imageWidth = image.Width;
                int imageHeight = image.Height;

                //获取当前单元格样式
                DataGridViewCellStyle cellStyle = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style;
                //设置单元格的Padding，确保图片不会紧贴单元格边框
                cellStyle.Padding = new Padding(2);
                //调整单元格大小来适应图片大小
                int cellWidth = imageWidth + cellStyle.Padding.Left + cellStyle.Padding.Right + 4;
                int cellHeight = imageHeight + cellStyle.Padding.Top + cellStyle.Padding.Bottom + 4;
                dataGridView.Rows[e.RowIndex].Height = Math.Max(dataGridView.Rows[e.RowIndex].Height, cellHeight);
                dataGridView.Columns[e.ColumnIndex].Width = Math.Max(dataGridView.Columns[e.ColumnIndex].Width, cellWidth);
            }
        }

        private void btn_GetCode_Click(object sender, EventArgs e)
        {
            CodeVector = new int[22];
            int code = 0;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        CodeVector[code] = 1;
                        code++;
                    }
                    else
                    {
                        CodeVector[code] = 0;
                        code++;
                    }
                }
            }
            label_Code.Text = "";
            for (int p = 0; p < listInt.Count(); p++)
            {
                label_Code.Text += listInt[p].ToString() + ",";
            }
            label_Code.Text += "\r\n";
            for (int k = 0; k < CodeVector.Count(); k++)
            {
                label_Code.Text += CodeVector[k].ToString() + ",";
            }
            string strResCode = "";
            int iRes = ConverCode(CodeVector, 22, out strResCode);
            textBox_ShowCode.Text = strResCode;
        }

        /// <summary>
        /// 根据协议转换点码 , 0为成功
        /// </summary>
        private int ConverCode(int[] intCode, int iNum, out string strRes)
        {
            strRes = "";
            //Convert Month
            string strMonth = ConvertBinaryInt(intCode, 22, 1, 4);
            int iMonth = StringConvertInt32(strMonth);
            //Convert Day
            string strDay = ConvertBinaryInt(intCode, 22, 5, 5);
            int iDay = StringConvertInt32(strDay);
            //Convert A/B shift
            string strAB = ConvertBinaryInt(intCode, 22, 10, 1);
            string strAOrB = "NUll";
            if (strAB.Contains("0"))
            {
                strAOrB = "A";
            }
            else if (strAB.Contains("1"))
            {
                strAOrB = "B";
            }
            if (strAOrB == "NUll")
            {
                return -1;
            }
            //Convert Material vendor
            string strMaterialv = ConvertBinaryInt(intCode, 22, 11, 1);
            if (strMaterialv.Contains("0"))
            {
                strMaterialv = "Kap";
            }
            else if (strMaterialv.Contains("1"))
            {
                strMaterialv = "Innovation";
            }
            //Convert Material type
            string strMaterialt = ConvertBinaryInt(intCode, 22, 12, 1);
            if (strMaterialt.Contains("0"))
            {
                strMaterialt = "6R01";
            }
            else if (strMaterialt.Contains("1"))
            {
                strMaterialt = "0Z13";
            }
            // Convert Line
            string strLine = ConvertBinaryInt(intCode, 22, 13, 4);
            int iLine = StringConvertInt32(strLine);
            //Convert Machine number
            string strMachineNo = ConvertBinaryInt(intCode, 22, 17, 6);
            int iMachineNo = StringConvertInt32(strMachineNo);

            //MakeResult
            strRes += (iMonth.ToString() + iDay.ToString() + strAOrB + strMaterialv + strMaterialt + iLine.ToString() + iMachineNo.ToString());
            return 0;
        }

        /// <summary>
        /// 截取拼接int数组
        /// </summary>
        /// <param name="intCode">int数组</param>
        /// <param name="iStart">从第几个开始</param>
        /// <param name="iNum">截取几个</param>
        /// <returns></returns>
        private string ConvertBinaryInt(int[] intCode, int iTolNum, int iStart, int iNum)
        {
            string strRes = "";
            if (iStart + iNum > iTolNum + 1)     //判断防呆
            {
                return strRes;
            }
            for (int i = iStart - 1; i < iStart - 1 + iNum; i++)
            {
                strRes += intCode[i].ToString();
            }
            return strRes;
        }

        /// <summary>
        /// 将可能带前导零的或不带前导零的二进制字符串转为二进制
        /// </summary>
        private int StringConvertInt32(string strGet)
        {
            foreach (char c in strGet)      //遍历判断是否为标准二进制字符串，是否只有1或0两种字符
            {
                if (c != '0' && c != '1')
                {
                    return -1;
                }
            }
            strGet = strGet.TrimStart('0');     //移除前导0的字符串
            if (strGet == "")
            {
                return 0;           //如果前导全是0，则返回0，例如"0000"，去掉前导0后则为""空字符串
            }
            int iRes = Convert.ToInt32(strGet, 2);
            return iRes;
        }
    }
}

