using System.ComponentModel;
using System.Windows.Forms;

namespace Smart.Win.Controls
{
    sealed partial class GlassButton
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerFade = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerFade
            // 
            this.timerFade.Interval = 10;
            this.timerFade.Tick += new System.EventHandler(this.timerFade_Tick);
            // 
            // CloudButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "CloudButton";
            this.Size = new System.Drawing.Size(150, 52);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CloudButtonOwn_MouseDown);
            this.Leave += new System.EventHandler(this.CloudButton_Leave);
            this.MouseEnter += new System.EventHandler(this.CloudButtonOwn_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CloudButtonOwn_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CloudButtonOwn_MouseUp);
            this.EnabledChanged += new System.EventHandler(this.CloudButton_EnabledChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Timer timerFade;
    }
}
