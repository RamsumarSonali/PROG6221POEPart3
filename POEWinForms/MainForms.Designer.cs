namespace POEWinForms
{
    partial class MainForms
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            btnSend = new Button();
            txtUserInput = new TextBox();
            rtbConversation = new RichTextBox();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(675, 385);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(100, 40);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // txtUserInput
            // 
            txtUserInput.Location = new Point(12, 393);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(650, 23);
            txtUserInput.TabIndex = 1;
            // 
            // rtbConversation
            // 
            rtbConversation.Location = new Point(12, 12);
            rtbConversation.Name = "rtbConversation";
            rtbConversation.ReadOnly = true;
            rtbConversation.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbConversation.Size = new Size(763, 365);
            rtbConversation.TabIndex = 2;
            rtbConversation.Text = "";
            // 
            // MainForms
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(rtbConversation);
            Controls.Add(txtUserInput);
            Controls.Add(btnSend);
            Name = "MainForms";
            Text = "Cybersecurity ChatBot";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private TextBox txtUserInput;
        private RichTextBox rtbConversation;
    }
}
