﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Commercial_automation_project1
{
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }
        sqlconnection sql_conn = new sqlconnection();
        private void Staff_List()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sql_adapter = new SqlDataAdapter("Select * From Table_Staff", sql_conn.Connention());
            sql_adapter.Fill(dt);
            gridControl1.DataSource = dt;


        }
        private void Staff_Load(object sender, EventArgs e)
        {
            Staff_List();
            Province_List();
            Clear();
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            SqlCommand cmd_save = new SqlCommand("insert into Table_Staff (AD,SOYAD,[TEL NO],[TC NO],MAİL,İL,İLÇE,ADRES,POZİSYON) values (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", sql_conn.Connention());
            cmd_save.Parameters.AddWithValue("@P1", textNAME.Text);
            cmd_save.Parameters.AddWithValue("@P2", textLastName.Text);
            cmd_save.Parameters.AddWithValue("@P3", textPHONE_NO.Text);
            cmd_save.Parameters.AddWithValue("@P4", textID_NO.Text);
            cmd_save.Parameters.AddWithValue("@P5", textMAIL.Text);
            cmd_save.Parameters.AddWithValue("@P6", cmb_Province.Text);
            cmd_save.Parameters.AddWithValue("@P7", cmb_County.Text);
            cmd_save.Parameters.AddWithValue("@P8", textADRESS.Text);
            cmd_save.Parameters.AddWithValue("@P9", textPosition.Text);
            cmd_save.ExecuteNonQuery();
            sql_conn.Connention().Close();
            MessageBox.Show("Personel bilgisi başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Staff_List();



        }
        private void Province_List()
        {

            SqlCommand cmd = new SqlCommand("Select PROVINCE From Provinces", sql_conn.Connention());
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmb_Province.Properties.Items.Add(reader[0]);

            }
            sql_conn.Connention().Close();
        }

        private void cmb_Province_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_County.Properties.Items.Clear();
            //
            SqlCommand cmd_c = new SqlCommand("Select COUNTY From Counties where PROVINCE=@P1", sql_conn.Connention());
            cmd_c.Parameters.AddWithValue("@P1", cmb_Province.SelectedIndex + 1);
            SqlDataReader reader = cmd_c.ExecuteReader();
            while (reader.Read())
            {
                cmb_County.Properties.Items.Add(reader[0]);
            }
            sql_conn.Connention().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                textID.Text = dr["ID"].ToString();
                textNAME.Text = dr["AD"].ToString();
                textLastName.Text = dr["SOYAD"].ToString();
                textPHONE_NO.Text = dr["TEL NO"].ToString();
                textID_NO.Text = dr["TC NO"].ToString();
                textMAIL.Text = dr["MAİL"].ToString();
                cmb_Province.Text = dr["İL"].ToString();
                cmb_County.Text = dr["İLÇE"].ToString();
                textPosition.Text = dr["POZİSYON"].ToString();
                textADRESS.Text = dr["ADRES"].ToString();
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Müşteri bilgisi silinecek emin misiniz!", "Bilgi", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);

            if (result == DialogResult.Yes)
            {
                SqlCommand cmd_delete = new SqlCommand("Delete From Table_Staff where ID=@P1", sql_conn.Connention());
                cmd_delete.Parameters.AddWithValue("@P1", textID.Text);
                cmd_delete.ExecuteNonQuery();
                sql_conn.Connention().Close();
                MessageBox.Show("Silme İşlemi başarıyla tamamlandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Staff_List();

            }
            else
            {
                MessageBox.Show("Silme İşlemi İptal Edilmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Staff_List();
            }
        }

        private void Button_Update_Click(object sender, EventArgs e)
        {
            SqlCommand cmd_update = new SqlCommand("update Table_Staff set AD=@P1,SOYAD=@P2,[TEL NO]=@P3,[TC NO]=@P4,MAİL=@P5,İL=@P6,İLÇE=@P7,ADRES=@P8,POZİSYON=@P9 WHERE ID=@P10 ",sql_conn.Connention());
            cmd_update.Parameters.AddWithValue("@P1", textNAME.Text);
            cmd_update.Parameters.AddWithValue("@P2", textLastName.Text);
            cmd_update.Parameters.AddWithValue("@P3", textPHONE_NO.Text);
            cmd_update.Parameters.AddWithValue("@P4", textID_NO.Text);
            cmd_update.Parameters.AddWithValue("@P5", textMAIL.Text);
            cmd_update.Parameters.AddWithValue("@P6", cmb_Province.Text);
            cmd_update.Parameters.AddWithValue("@P7", cmb_County.Text);
            cmd_update.Parameters.AddWithValue("@P8", textADRESS.Text);
            cmd_update.Parameters.AddWithValue("@P9", textPosition.Text);
            cmd_update.Parameters.AddWithValue("@P10",textID.Text);
            cmd_update.ExecuteNonQuery();
            sql_conn.Connention().Close();
            MessageBox.Show("Bilgiler başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Staff_List();
        }
        private void Clear()
        {
            textNAME.Text = "";
            textLastName.Text = "";
            textPHONE_NO.Text = "";
            textID_NO.Text = "";
            textMAIL.Text = "";
            cmb_Province.Text = "";
            cmb_County.Text = "";
            textADRESS.Text = "";
            textPosition.Text = "";
            textID.Text = "";
        }
            

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Clear();
        }
       
    }
}
