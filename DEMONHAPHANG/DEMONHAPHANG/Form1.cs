using QLCHNH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEMONHAPHANG
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        classketnoi kn = new classketnoi();
        DataTable dt = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            kn.FILLComboBox("select MANCC, TENNCC from NHACUNGCAP",cbnhacc,"MANCC","TENNCC");
            cbnhacc.SelectedIndex = -1;
            kn.FILLComboBox("select MALOAI,TENLOAI FROM LOAI", cbloainuochoa, "MALOAI", "TENLOAI");
            cbloainuochoa.SelectedIndex = -1;
            kn.FILLComboBox("select MATHUONGHIEU , TENTHUONGHIEU FROM THUONGHIEU", cbthuonghieu, "MATHUONGHIEU", "TENTHUONGHIEU");
            cbthuonghieu.SelectedIndex = -1;
            kn.FILLComboBox("select MACHIETKHAU , PHANTRAMCK FROM CHIETKHAU", cbchietkhau, "MACHIETKHAU", "PHANTRAMCK");
            cbchietkhau.SelectedIndex = -1;
            kn.FILLComboBox("select MANVIEN , HOTEN FROM NHANVIEN", cbnhanvien, "MANVIEN", "HOTEN");
            cbnhanvien.SelectedIndex = -1;
            tbtongtien.Text = "0";
            tbgianhap.ReadOnly = true;
            
        //    hietthiGridview();

            //vi du gia nhap la 1000000;
            tbgianhap.ReadOnly = true;
            tbgianhap.Text = "1000000";
            button1.Enabled = false;


        }
        private void hietthiGridview()
        {
            kn = new classketnoi();
            string sql = "select b.MANUOCHOA , a.TENNUOCHOA ,b.SLNUOCHOANHAP , b.MACHIETKHAU ,b.DONGIANHAP , b.THANHTIENNHAP FROM NUOCHOA AS a, CHITIETNHAP AS b Where b.MAPHIEUNHAP ='"+tbMAPHIEUNHAP.Text+"' AND a.MANUOCHOA = b.MANUOCHOA ";
            
            dt = kn.load_du_lieu(sql);
            dgvNhap.DataSource = dt;
        }
        private void loadnuochoaCoMaTrung()
        {
            cbloainuochoa.SelectedValue = kn.GetValues("select MALOAI FROM NUOCHOA WHERE MANUOCHOA = '"+tbmanuochoa.Text+"'");
            cbthuonghieu.SelectedValue = kn.GetValues("select MATHUONGHIEU FROM NUOCHOA WHERE MANUOCHOA = '" + tbmanuochoa.Text + "'");
            tbdungtich.Text = kn.GetValues("select DUNGTICH FROM NUOCHOA WHERE MANUOCHOA = '" + tbmanuochoa.Text + "'");
        }
        private void resetdulieu()
        {
            cbloainuochoa.SelectedIndex = -1;
            tbthanhtien.Text = "";
            cbthuonghieu.SelectedItem = -1;
            tbdungtich.Text = "";
            cbchietkhau.SelectedItem = -1;
            tbsoluongnhap.Text = "";
            tbthanhtien.Text = "";
            tbmanuochoa.Text = "";
            cbnhanvien.SelectedItem = -1;
            cbnhacc.SelectedIndex = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            double  tong, tongmoi;
            sql = "select MAPHIEUNHAP FROM PHIEUNHAP WHERE MAPHIEUNHAP ='" + tbMAPHIEUNHAP.Text + "'";
            if (!kn.CheckKey(sql))
            {
                if (cbnhanvien.Text == "")
                {
                    MessageBox.Show("chua co nhan vien");
                    cbnhanvien.Focus();
                    return;
                }
                if (cbnhacc.Text == "")
                {
                    MessageBox.Show("chua co nha cc");
                    cbnhacc.Focus();
                    return;
                }
                sql = "insert into PHIEUNHAP VALUES('" + tbMAPHIEUNHAP.Text + "','" + cbnhanvien.SelectedValue + "','" + cbnhacc.SelectedValue + "','" + ngaynhap.Value.ToShortDateString() + "'," + tbtongtien.Text + ")";
                /// kiem tra ma hang trong text bot ma hang moi
                kn.thaotaclenh(sql);
            }
            string ma = tbmanuochoa.Text.Trim();
            sql = "select  * from NUOCHOA WHERE MANUOCHOA = '" + ma + "'";
            if (!kn.CheckKey(sql))
            {
                if (cbloainuochoa.Text == "")
                {
                    MessageBox.Show("ban can nhap loai cho nuoc hoa moi");
                    cbloainuochoa.Focus();
                    return;
                }
                if (cbthuonghieu.Text == "")
                {
                    MessageBox.Show("ban can nhap thuong hieu cho nuoc hoa moi");
                    cbthuonghieu.Focus();
                    return;
                }
                if (tbtensp.Text == "")
                {
                    MessageBox.Show("ban can nhap ten cho nuoc hoa moi");
                    tbtensp.Focus();
                    return;
                }
                if (tbdungtich.Text == "")
                {
                    MessageBox.Show("ban can nhap dung tich cho nuoc hoa moi");
                    tbdungtich.Focus();
                    return;
                }


                sql = "insert into NUOCHOA (MANUOCHOA,MALOAI,MATHUONGHIEU,TENNUOCHOA,SOLUONGTON,GIABANDEXUAT,DUNGTICH) VALUES ('"+tbmanuochoa.Text+"','"+cbloainuochoa.SelectedValue+"','"+cbthuonghieu.SelectedValue+"','"+tbtensp.Text+"',0,0,'"+tbdungtich.Text+"')";
                kn.thaotaclenh(sql);
            }
            else
            {
                MessageBox.Show("tien hanh load du lieu da co trong may");
                loadnuochoaCoMaTrung();
                // neu ma trung thi ta load du lieu cua cai ma cu len
                // getvalues ;
                // update du lieu vao soluong
            }
            //////
           

            

            if (tbsoluongnhap.Text == "")
            {
                MessageBox.Show("ban can nhap soluongnhap cho nuoc hoa moi");
                tbsoluongnhap.Focus();
                return;
            }

            sql = "select  MANUOCHOA from CHITIETNHAP WHERE MANUOCHOA = '" + ma + "' AND MAPHIEUNHAP ='"+tbMAPHIEUNHAP.Text.Trim()+"'";

            if(kn.CheckKey(sql))
            {
                MessageBox.Show("ma san pham da co trong phieu nhap ");
                tbmanuochoa.Focus();
                return;
            }
            if (int.Parse(tbsoluongnhap.Text)<=0)
            {
                MessageBox.Show("so luong nhap phai >0");
                tbsoluongnhap.Text = "";
                tbsoluongnhap.Focus();
                return;
            }
            sql = "insert into CHITIETNHAP(MAPHIEUNHAP,MANUOCHOA,MACHIETKHAU,SLNUOCHOANHAP,DONGIANHAP,THANHTIENNHAP) values('" + tbMAPHIEUNHAP.Text+"','"+tbmanuochoa.Text+"','"+cbchietkhau.SelectedValue+"','"+tbsoluongnhap.Text+"','"+tbgianhap.Text+"','"+tbthanhtien.Text+"')";

            kn.thaotaclenh(sql);
            hietthiGridview();
            //capnhatsoluongton
            int sl =int.Parse( kn.GetValues("select SOLUONGTON FROM NUOCHOA WHERE MANUOCHOA ='"+tbmanuochoa.Text+"'"));
            int slcon = int.Parse(tbsoluongnhap.Text)+sl;

            sql = "update NUOCHOA SET SOLUONGTON =" + slcon + " where MANUOCHOA ='" + tbmanuochoa.Text.Trim() + "'";
            kn.thaotaclenh(sql);

             tong = Convert.ToDouble(kn.GetValues("select TONGTIENNHAP FROM PHIEUNHAP WHERE MAPHIEUNHAP ='"+tbMAPHIEUNHAP.Text+"'"));
            tongmoi = tong + Convert.ToDouble(tbthanhtien.Text);

            sql = "update PHIEUNHAP SET TONGTIENNHAP =" + tongmoi + "where MAPHIEUNHAP ='"+tbMAPHIEUNHAP.Text+"'";

            kn.thaotaclenh(sql);
            tbtongtien.Text = tongmoi.ToString();
            resetdulieu();
           
            themmoi.Enabled = true;
            
            // resert value();
          //  tbMAPHIEUNHAP.Text = kn.CreateKey("NH_");
          //


        }

        private void tbmanuochoa_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbnhacc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbsoluongnhap_TextChanged(object sender, EventArgs e)
        {
            Double tt, sl, dg, ck;
            if (tbsoluongnhap.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(tbsoluongnhap.Text);

            if (cbchietkhau.Text == "")
                ck = 0;
            else
                ck = Convert.ToDouble(cbchietkhau.Text);
            if(tbgianhap.Text=="")
                dg = 0;
            else
                dg = Convert.ToDouble(tbgianhap.Text);
            tt = sl * dg - sl * ck * dg / 100;
            tbthanhtien.Text = tt.ToString();

        }

        private void btbosanoham_Click(object sender, EventArgs e)
        {
            // khi bo san pham trong phieu nhap
            // if khoa trung thi ta tien hanh - so luong voi so luong trong gidview
            // khong trung thi tien hanh xoa sanr pham theo ma san pham trong phieu nhap
            // cap nhat lai tong tien va thanh tien khi xoa
            /*string sql = "select b.MANUOCHOA , a.TENNUOCHOA ,b.SLNUOCHOANHAP ,
             * b.MACHIETKHAU ,b.DONGIANHAP , b.THANHTIENNHAP 
             * FROM NUOCHOA AS a, CHITIETNHAP AS b 
             */
            double sl, slcon, slxoa; string sql;
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //select a.MANUOCHOA ,b.GIABANDEXUAT ,a.SLNUOCHOANHAP from NUOCHOA as b , CHITIETNHAP as a where a.MANUOCHOA = b.MANUOCHOA;

                // sql = "SELECT b.MANUOCHOA ,a.GIABANDEXUAT ,b.SLNUOCHOANHAP FROM NUOCHOA AS a, CHITIETNHAP AS b WHERE a.MANUOCHOA = b.MANUOCHOA ";

                //select a.MANUOCHOA , b.GIABANDEXUAT FROM NUOCHOA AS a, CHITIETNHAP AS b Where b.MAPHIEUNHAP ='"+tbMAPHIEUNHAP.Text+"' AND a.MANUOCHOA = b.MANUOCHOA 
                sql = "SELECT MANUOCHOA,SLNUOCHOANHAP FROM CHITIETNHAP WHERE MAPHIEUNHAP = '" + tbMAPHIEUNHAP.Text+"' ";

                DataTable tblHang = kn.load_du_lieu(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    if (kn.GetValues("SELECT GIABANDEXUAT FROM NUOCHOA WHERE MANUOCHOA = '" + tblHang.Rows[hang][0].ToString() + "'")=="0")
                    {
                        sql = "DELETE CHITIETNHAP WHERE MANUOCHOA='" + tblHang.Rows[hang][0].ToString() + "'";
                        kn.thaotaclenh(sql);

                        sql = "DELETE NUOCHOA WHERE MANUOCHOA='"+ tblHang.Rows[hang][0].ToString() + "'";
                        kn.thaotaclenh(sql);
                    }
                    else
                    {
                        // Cập nhật lại số lượng cho các mặt hàng
                        sl = Convert.ToDouble(kn.GetValues("SELECT SOLUONGTON FROM NUOCHOA WHERE MANUOCHOA = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                        slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                        slcon = Math.Abs(sl - slxoa);
                        sql = "UPDATE NUOCHOA SET SOLUONGTON =" + slcon + " WHERE MANUOCHOA= N'" + tblHang.Rows[hang][0].ToString() + "'";
                        kn.thaotaclenh(sql);
                    }
                }
                sql = "DELETE CHITIETNHAP WHERE MAPHIEUNHAP=N'" + tbMAPHIEUNHAP.Text + "'";
                kn.thaotaclenh(sql);

                //Xóa hóa đơn
                sql = "DELETE PHIEUNHAP WHERE MAPHIEUNHAP=N'" + tbMAPHIEUNHAP.Text + "'";
                kn.thaotaclenh(sql);


                hietthiGridview();
            }
        }

        private void themmoi_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            tbMAPHIEUNHAP.Text = kn.CreateKey("NH_");
            themmoi.Enabled = false;
            resetdulieu();
            hietthiGridview();
        }

        private void dgvNhap_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvNhap.CurrentRow.Cells["MANUOCHOA"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvNhap.CurrentRow.Cells["SLNUOCHOANHAP"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvNhap.CurrentRow.Cells["THANHTIENNHAP"].Value.ToString());
                sql = "DELETE CHITIETNHAP WHERE MAPHIEUNHAP='" + tbMAPHIEUNHAP.Text + "' AND MANUOCHOA = N'" + MaHangxoa + "'";
                kn.thaotaclenh(sql);


                if (kn.GetValues("SELECT GIABANDEXUAT FROM NUOCHOA WHERE MANUOCHOA = '" +MaHangxoa+ "'") == "0")
                {
                    sql = "DELETE CHITIETNHAP WHERE MANUOCHOA='" + MaHangxoa + "'";
                    kn.thaotaclenh(sql);

                    sql = "DELETE NUOCHOA WHERE MANUOCHOA='" + MaHangxoa + "'";
                    kn.thaotaclenh(sql);
                }
                else
                {

                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(kn.GetValues("SELECT SOLUONGTON FROM NUOCHOA WHERE MANUOCHOA = N'" + MaHangxoa + "'"));
                    slcon = Math.Abs(sl - SoLuongxoa);
                    sql = "UPDATE NUOCHOA SET SOLUONGTON =" + slcon + " WHERE MANUOCHOA= N'" + MaHangxoa + "'";
                    kn.thaotaclenh(sql);
                    // Cập nhật lại tổng tiền cho hóa đơn bán

                }




                tong = Convert.ToDouble(kn.GetValues("SELECT TONGTIENNHAP FROM PHIEUNHAP WHERE MAPHIEUNHAP='" + tbMAPHIEUNHAP.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                sql = "UPDATE PHIEUNHAP SET TONGTIENNHAP =" + tongmoi + " WHERE MAPHIEUNHAP='" + tbMAPHIEUNHAP.Text + "'";
                kn.thaotaclenh(sql);
                tbtongtien.Text = tongmoi.ToString();
                //lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChu(tongmoi.ToString());
                hietthiGridview();                  
            }
        }
    }
}
