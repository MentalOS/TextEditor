﻿#region USING
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
#endregion

namespace TextEditor
{
    public partial class textEditor : Form
    {

        private string filepath = null;
        private StreamReader sPrint;
        private StreamReader sr = null;
        private StreamWriter sw = null;

        public textEditor()
        {
            InitializeComponent();
        }

     #region File Dropdown

        private void mNew_Click(object sender, EventArgs e)
        {
            NewFile();
            txtMessage.Text = "Here is a new document for you to type in. Enjoy...";
        }

        private void mOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
            txtMessage.Text = "The file \"" + dOpen.SafeFileName + "\" has been opened. OH BOY, OH BOY, OH BOY";
        }

        private void mSave_Click(object sender, EventArgs e)
        {
            SaveFile();
            txtMessage.Text = "The file \"" + dSave.FileName + "\" has been saved. JOLLY GOOD SHOW";
        }

        private void mSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileAs();
            txtMessage.Text = "The file \"" + dSave.FileName + "\" has been saved. JOLLY GOOD SHOW";
        }

        private void mPrint_Click(object sender, EventArgs e)
        {
            PrintFile();
            txtMessage.Text = "Document Printed.  No really it's printed.";
        }

        private void mExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

     #endregion

     #region Edit Dropdown

        private void mUndo_Click(object sender, EventArgs e)
        {
            txt.Undo();
            txtMessage.Text = "Woe is me.  What have I done?";
        }

        private void mCut_Click(object sender, EventArgs e)
        {
            txt.Cut();
            txtMessage.Text = "Are you absolutly positive that you want to do that?";
        }

        private void mCopy_Click(object sender, EventArgs e)
        {
            txt.Copy();
            txtMessage.Text = "Rub a dub dub, 2 in a tub.  Text Copied";
        }

        private void mPaste_Click(object sender, EventArgs e)
        {
            txt.Paste();
            txtMessage.Text = "SPLAT.....  Your Text has been pasted";
        }

        private void mSelectAll_Click(object sender, EventArgs e)
        {
            txt.SelectAll();
            txtMessage.Text = "Well thats done so what are you going to do with this text now?";
        }

        private void mInsertDateTime_Click(object sender, EventArgs e)
        {
            int CaretPosition = txt.SelectionStart;
            string TextBefore = txt.Text.Substring(0, CaretPosition);
            string textAfter = txt.Text.Substring(CaretPosition);
            string currentDate = " " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + " "; 
            txt.Text = TextBefore + currentDate + textAfter;
        }

     #endregion

     #region Tools Dropdown

        private void mFont_Click(object sender, EventArgs e)
        {
            dFont.ShowColor = true;
            if (dFont.ShowDialog() == DialogResult.OK & !String.IsNullOrEmpty(txt.Text))
            {
                txt.SelectionFont = dFont.Font;
                txt.SelectionColor = dFont.Color;
            }
        }

        private void mToolbar_Click(object sender, EventArgs e)
        {
            if (toolbar.Visible == true)
            {
                toolbar.Visible = false;
            }
            else
            {
                toolbar.Visible = true;
            }
        }

        private void mFind_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "Not Yet Implemented";
        }

        private void mInsertLink_Click(object sender, EventArgs e)
        {
            txtMessage.Text = "Not Yet Implemented";
        }

     #endregion

     #region About Dropdown

        private void mAbout_Click(object sender, EventArgs e)
        {
            AboutTextEditor about = new AboutTextEditor();
            about.Show();
        }

        private void mAboutComTech_Click(object sender, EventArgs e)
        {
            AboutComTech aboutComtech = new AboutComTech();
            aboutComtech.Show();
        }


        private void mReleaseNotes_Click(object sender, EventArgs e)
        {
            ReleaseNotes rNotes = new ReleaseNotes();
            rNotes.Show();
        }

        #endregion

     #region Toolbar

        #region FONT
        private void ptCalibri_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font("Calibri", txt.Font.Size);
        }

        private void ptConsolas_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font("Consolas", txt.Font.Size);
        }

        private void ptTimes_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font("Times", txt.Font.Size);
        }
        #endregion

        #region SIZE
        private void ptSize8_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 8);
        }

        private void ptSize9_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 9);
        }

        private void ptSize10_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 10);
        }

        private void ptSize11_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 11);
        }

        private void ptSize12_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 12);
        }

        private void ptSize14_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 14);
        }

        private void ptSize16_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 16);
        }

        private void ptSize24_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 24);
        }

        private void ptSize36_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 36);
        }

        private void ptSize48_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 48);
        }

        private void ptSize72_Click(object sender, EventArgs e)
        {
            txt.SelectionFont = new Font(txt.SelectionFont.FontFamily, 72);
        }
        #endregion

        #region STYLE
        private void ptBold_Click(object sender, EventArgs e)
        {
            //If the selected character is Bold 
            if (txt.SelectionFont.Style.ToString().Contains("Bold"))
            {
                txt.SelectionFont = new Font(txt.SelectionFont, txt.SelectionFont.Style & ~FontStyle.Bold);
            }
            //If the selected character is unBold Make the selected character Bold
            else
            {
                txt.SelectionFont = new Font(txt.SelectionFont, txt.SelectionFont.Style | FontStyle.Bold);
            }
        }

        private void ptItalic_Click(object sender, EventArgs e)
        {
            //If the selected character is Italic 
            if (txt.SelectionFont.Style.ToString().Contains("Italic"))
            {
                txt.SelectionFont = new Font(txt.SelectionFont, txt.SelectionFont.Style & ~FontStyle.Italic);
            }
            //If the selected character is not Italic Make the selected character Italic
            else
            {
                txt.SelectionFont = new Font(txt.SelectionFont, txt.SelectionFont.Style | FontStyle.Italic);
            }
        }

        private void ptUnderline_Click(object sender, EventArgs e)
        {
            //If the selected character is Underline 
            if (txt.SelectionFont.Style.ToString().Contains("Underline"))
            {
                txt.SelectionFont = new Font(txt.SelectionFont, txt.SelectionFont.Style & ~FontStyle.Underline);
            }
            //If the selected character is not underline Make the selected character Underline
            else
            {
                txt.SelectionFont = new Font(txt.SelectionFont, txt.SelectionFont.Style | FontStyle.Underline);
            }
        }
        #endregion

     #endregion

     #region Contextual Menu 

        private void cmCut_Click(object sender, EventArgs e)
        {
            txt.Cut();
        }

        private void cmCopy_Click(object sender, EventArgs e)
        {
            txt.Copy();
        }

        private void cmPaste_Click(object sender, EventArgs e)
        {
            txt.Paste();
        }

        private void cmSourceCode_Click(object sender, EventArgs e)
        {
            string rtfcode = string.Empty;
            rtfcode = txt.SelectedRtf;
            MessageBox.Show(rtfcode);
        }

        private void cmClose_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

     #endregion

     #region New Methods

        private void NewFile()
        {
            if (txt.Modified)
            {
                DialogResult r = MessageBox.Show(this,
                                                        "Save current documemt?",
                                                        "Save",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Question,
                                                        MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Yes) SaveFile();
             }
             txt.Clear();
        }

     #endregion

     #region Save and Save As Methods

        private void SaveFile()
        {

           if (filepath == null)
            {
                SaveFileAs(); return;
            }

            try
            {
                sw = new StreamWriter(filepath);
                sw.WriteLine(txt.Rtf);
                txt.Modified = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to save your file because: \n" + e.Message);
            }
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        private void SaveFileAs()
        {
            dSave.Title = "Save the currnet file";
            dSave.Filter = "Rich Text File|*.rtf|All File Types|*.*";

            if (dSave.ShowDialog() != DialogResult.OK) return;
            filepath = dSave.FileName;
            SaveFile();
        }

        #endregion

     #region Open Methods

        private void OpenFile()
        {
            NewFile();

            dOpen.Title = "Select as File to open";
            dOpen.Filter = "Rich Text File|*.rtf|All File Types|*.*";


            if (dOpen.ShowDialog() != DialogResult.OK) return;

            try
            {
                sr = new StreamReader(File.OpenRead(dOpen.FileName));
                txt.Rtf = sr.ReadToEnd();

                filepath = dOpen.FileName;

                txt.SelectAll();
                txt.SelectionFont = new Font("Calibri", 12);
                txt.Select(0, 0);
                MoveCursor();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to open your file because: \n" + e.Message);
                txtMessage.Text = "ERROR file \"" + dOpen.SafeFileName + "\" cannot be opened.";
            }
            finally
            {
                if (sr != null) sr.Close();
            }

        }
        #endregion

     #region MOVE CURSOR
        private void MoveCursor()
        {
            // Set the Current cursor, move the cursor's Position, 
            // and set its clipping rectangle to the form.  

            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
            //Cursor.Clip = new Rectangle(this.Location, this.Size);
        }
        #endregion

     #region Print Methods

        private void PrintFile()
        {
            if (dPrint.ShowDialog() != DialogResult.OK) return;
            try
            {
                sPrint = new StreamReader(
                         new MemoryStream(
                             Encoding.ASCII.GetBytes(txt.Text)));

                printDocument.PrintPage
                    += new PrintPageEventHandler(PrintPage);
                printDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to print your file \n" + e.Message);
            }
            finally
            {
                if (sPrint != null) sPrint.Close();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = e.MarginBounds.Height / txtMessage.Font.GetHeight(e.Graphics);

            for (int count = 0;
                count <linesPerPage && !sPrint.EndOfStream;
                count++)
            {
                e.Graphics.DrawString(sPrint.ReadLine(),
                                      txt.Font,
                                      Brushes.Black,
                                      e.MarginBounds.Left,
                                      e.MarginBounds.Top + (count *
                                      txt.Font.GetHeight(e.Graphics)),
                                      new StringFormat());
            }

            e.HasMorePages = !sPrint.EndOfStream;
        }


        #endregion

    }
}
