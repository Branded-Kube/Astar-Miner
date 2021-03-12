using Microsoft.Xna.Framework.Graphics;
using System;

namespace Vupa
{
    public partial class Form
    {

        private VisualManager visualManager;
        public event EventHandler Click;


        public Form()
        {
            //InitializeComponent();

            //ClientSize = new Size(800, 800);

            //visualManager = new VisualManager(CreateGraphics(), this.DisplayRectangle);
        }

        //private void loop_Tick(object sender, EventArgs e, SpriteBatch spriteBatch)
        //{
        //    visualManager.Render(spriteBatch);
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        //private void Form1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    visualManager.ClickCell(this.PointToClient(Cursor.Position));
        //}


        // moved to game1
        //private void Form1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Space)
        //    {
        //        visualManager.FindPath();
        //    }
        //}

    }
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.loop = new System.Windows.Forms.Timer(this.components);
        //    this.SuspendLayout();
        //    // 
        //    // loop
        //    // 
        //    this.loop.Enabled = true;
        //    this.loop.Tick += new System.EventHandler(this.loop_Tick);
        //    // 
        //    // Form1
        //    // 
        //    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(604, 560);
        //    this.Name = "Form1";
        //    this.Text = "Form1";
        //    this.Load += new System.EventHandler(this.Form1_Load);
        //    this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
        //    this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
        //    this.ResumeLayout(false);
        //}

        #endregion

        private System.Windows.Forms.Timer loop;
    }
}

