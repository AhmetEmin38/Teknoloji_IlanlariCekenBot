using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading;
using System.Net;

namespace IlanÇekenbot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.DefaultCellStyle.Font = new Font("DIN2014-☞", 10);
            this.dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(0, 192, 192);
            this.dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(0, 38, 38);
        }
        int move;
        int mouse_X;
        int mouse_Y;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            mouse_X = e.X;
            mouse_Y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_X, MousePosition.Y - mouse_Y);
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox1.Text == "")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else if (bunifuMetroTextbox1.Text == "Aramak İçin Tıklayın")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else
            {
                label2.Visible = false;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                label1.Visible = false;
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "Başlık";
                dataGridView1.Columns[1].Name = "Fiyat";
                dataGridView1.ClearSelection();
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                string link = "https://www.sinerji.gen.tr/" + bunifuMetroTextbox1.Text + "-s";
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);
                string baslik = "";
                string fiyat = "";
                for (int i = 1; i <=24; i++)
                {
                    try
                    {
                        baslik = document.DocumentNode.SelectSingleNode($"/html/body/div[1]/div[2]/div[2]/section/div[{i}]/article/div[2]/a").InnerText;
                        try
                        {
                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[1]/div[2]/div[2]/section/div[{i}]/article/div[3]/div[1]/span[1]").InnerText;
                            fiyat.Remove(0, 7);
                        }
                        catch (Exception)
                        {
                            fiyat = "Stokta Yok ";

                        }
                        dataGridView1.Rows.Add(baslik, fiyat);
                    }
                    catch (Exception)
                    {
                        
                        label2.Visible = true;
                        break;
                    }
                    
                }
            }
        }

        private void bunifuMetroTextbox1_Click(object sender, EventArgs e)
        {
            bunifuMetroTextbox1.Text = "";
            bunifuMetroTextbox1.ForeColor = Color.FromArgb(0,150,150);
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            bunifuMetroTextbox1.ForeColor = Color.FromArgb(0, 150, 150);
        }

        private void bunifuMetroTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

            }
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            
                if (bunifuMetroTextbox1.Text == "")
                {
                    label1.Visible = true;
                     SystemSounds.Beep.Play();
                }
                else if (bunifuMetroTextbox1.Text == "Aramak İçin Tıklayın")
                {
                    label1.Visible = true;
                     SystemSounds.Beep.Play();
                }
            else
            {
                label2.Visible = false;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                label1.Visible = false;
                dataGridView1.ColumnCount = 3;
                dataGridView1.Columns[0].Name = "Fiyat";
                dataGridView1.Columns[1].Name = "Başlık";
                dataGridView1.Columns[2].Name = "Durum";
                
                dataGridView1.ClearSelection();
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                string link = "https://www.itopya.com/AramaSonuclari?text=" + bunifuMetroTextbox1.Text;
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);
                string durum = "";
                string baslik = "";
                string fiyat = "0";
                bool stok = false;

                
                for (int k = 1; k < 2; k++)
                {
                    
                    for (int i = 1; i <= 20; i++)
                    {
                        
                        try
                        {
                            try
                            {
                                durum = document.DocumentNode.SelectSingleNode($"/html/body/section[2]/div/div[2]/div[2]/div[{i}]/div[2]/strong").InnerText;
                                if (durum == "İncele")
                                {
                                    goto abbc;
                                }
                                else if (durum == "incele")
                                {
                                    goto abbc;
                                }
                            }
                            catch (Exception ex)
                            {
                                label2.Visible = true;
                                SystemSounds.Beep.Play();

                                break;
                            }

                            baslik = document.DocumentNode.SelectSingleNode($"/html/body/section[2]/div/div[2]/div[2]/div[{i}]/div[3]/a").InnerText;
                            try
                            {
                                if (Convert.ToBoolean(fiyat = Convert.ToString(document.DocumentNode.SelectSingleNode($"/html/body/section[2]/div/div[2]/div[2]/div[{i}]/div[4]/div[2]/strong").InnerText != null)))
                                {
                                    fiyat = document.DocumentNode.SelectSingleNode($"/html/body/section[2]/div/div[2]/div[2]/div[{i}]/div[4]/div[2]/strong").InnerText;
                                }
                                else if (Convert.ToBoolean(fiyat = Convert.ToString(document.DocumentNode.SelectSingleNode($"/html/body/section[2]/div/div[2]/div[2]/div[{i}]/div[4]/div[2]/strong").InnerText == null)))
                                {
                                    fiyat = "0";
                                }


                            }
                            catch (Exception)
                            {
                                fiyat = "0";
                            }
                        abbc:

                            dataGridView1.Rows.Add(fiyat, baslik, durum);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }

                    }
                }
                                   

            }
            


        }

        private void bunifuMetroTextbox1_Enter(object sender, EventArgs e)
        {
            bunifuMetroTextbox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox1.Text == "")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else if (bunifuMetroTextbox1.Text == "Aramak İçin Tıklayın")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else
            {
                label2.Visible = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "Başlık";
                dataGridView1.Columns[1].Name = "Fiyat";
                dataGridView1.ClearSelection();
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                string link = "https://www.incehesap.com/q/" + bunifuMetroTextbox1.Text + "/";
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);
                string fiyat = "";
                string baslik = "";

                for (int i = 1; i <= 24; i++)
                {
                    try
                    {
                        baslik = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div[2]/main/section/div/section[2]/div[1]/div[{i}]/a/span[1]").InnerText;
                        try
                        {
                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div[2]/main/section/div/section[2]/div[1]/div[{i}]/a/span[2]/span/span/span[2]").InnerText;
                        }
                        catch (Exception)
                        {

                        }
                        try
                        {
                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div[2]/main/section/div/section[2]/div[1]/div[{i}]/a/span[2]/span/span[2]/span[2]").InnerText;

                        }
                        catch (Exception)
                        {

                        }
                        try
                        {
                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div[2]/main/section/div/section[2]/div[1]/div[{i}]/div/div").InnerText;

                        }
                        catch (Exception)
                        {

                        }
                    }
                    catch (Exception)
                    {
                        label2.Visible = true;
                        break;
                    }
                    
                    

                    dataGridView1.Rows.Add(baslik, fiyat);
                }                                                     

            }
            

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox1.Text == "")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else if (bunifuMetroTextbox1.Text == "Aramak İçin Tıklayın")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else
            {
                label2.Visible = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 3;
                dataGridView1.Columns[0].Name = "Başlık";
                dataGridView1.Columns[1].Name = "Fiyat";
                dataGridView1.Columns[2].Name = "Stok";
                dataGridView1.ClearSelection();
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                string link = "https://www.tebilon.com/arama/?a=" + bunifuMetroTextbox1.Text;
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);
                string fiyat = "";
                string baslik = "";
                string durum = "";
                for (int i = 1; i <= 40; i++)
                {
                    try
                    {
                        baslik = document.DocumentNode.SelectSingleNode($"/html/body/div[5]/div[1]/main/section[2]/div/div/div[2]/div[5]/div[1]/div/div/div/div[{i}]/div/div/div/div[2]/div[1]/a").InnerText;
                        try
                        {
                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[5]/div[1]/main/section[2]/div/div/div[2]/div[5]/div[1]/div/div/div/div[{i}]/div/div/div/div[2]/div[4]/div/div/div[2]").InnerText;
                        }
                        catch (Exception)
                        {
                            fiyat = "0";
                            
                        }
                        durum = document.DocumentNode.SelectSingleNode($"/html/body/div[5]/div[1]/main/section[2]/div/div/div[2]/div[5]/div[1]/div/div/div/div[{i}]/div/div/div/div[2]/div[5]/div/div[4]/a").InnerText;

                        dataGridView1.Rows.Add(baslik, fiyat, durum);
                    }                                                      
                    catch (Exception)
                    {
                        label2.Visible = true;
                        break;
                    }
                    

                }                                                      
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (bunifuMetroTextbox1.Text == "")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else if (bunifuMetroTextbox1.Text == "Aramak İçin Tıklayın")
            {
                label1.Visible = true;
                SystemSounds.Beep.Play();
            }
            else
            {
                label2.Visible = false;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "Başlık";
                dataGridView1.Columns[1].Name = "Fiyat";
                dataGridView1.ClearSelection();
                dataGridView1.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                string link = "https://www.gaming.gen.tr/?s=" + bunifuMetroTextbox1.Text + "&post_type=product&dgwt_wcas=1";
                Uri url = new Uri(link);
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);
                string fiyat = "";
                string baslik = "";
                for (int i = 1; i <= 18; i++)
                {
                    try
                    {
                        baslik = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div/div[1]/main/article/div/div/div[2]/ul/li[{i}]/a/h2").InnerText;
                        try
                        {
                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div/div[1]/main/article/div/div/div[2]/ul/li[{i}]/a/span/span/bdi").InnerText;
                        }
                        catch (Exception)
                        {

                            fiyat = document.DocumentNode.SelectSingleNode($"/html/body/div[2]/div/div[1]/main/article/div/div/div[2]/ul/li[{i}]/a/span/ins/span/bdi").InnerText;
                        }
                        
                                                                         
                        dataGridView1.Rows.Add(baslik, fiyat);                                                
                    }                                                     
                    catch (Exception)
                    {
                        label2.Visible = true;
                        break;
                    }
                }
            }
              
        }
    }
}
