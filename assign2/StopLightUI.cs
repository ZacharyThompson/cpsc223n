using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class StopLightUI : Form
{
	private Panel header_panel = new Panel();
	private Graphic_Panel display_panel = new Graphic_Panel();
	private Panel control_panel = new Panel();

	private Label title = new Label();

	private Button start_button = new Button();
	private Button fast_button = new Button();
	private Button slow_button = new Button();
	private Button pause_button = new Button();
	private Button exit_button = new Button();

	private Size max_win_size = new Size(300,900);
	private Size min_win_size = new Size(300,900);

	public StopLightUI()
	{
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;


		Text = "Stop Light";
		title.Text = "Stop Light Simulator by Zachary Thompson";
		start_button.Text = "Start";
		fast_button.Text = "Fast";
		slow_button.Text = "Slow";
		pause_button.Text = "Pause";
		exit_button.Text = "Exit";

		Size = MinimumSize;
		header_panel.Size  = new Size(300,100);
		display_panel.Size  = new Size(300,700);
		control_panel.Size = new Size(300,100);
		start_button.Size = new Size(60,30);
		fast_button.Size = new Size(60,30);
		slow_button.Size = new Size(60,30);
		pause_button.Size = new Size(60,30);
		exit_button.Size = new Size(60,30);

		header_panel.BackColor = Color.Yellow;
		display_panel.BackColor = Color.LightGray;
		control_panel.BackColor = Color.LightGreen;

		title.Font = new Font("Arial",13,FontStyle.Regular);
		start_button.Font = new Font("Arial",13,FontStyle.Regular);
		fast_button.Font = new Font("Arial",13,FontStyle.Regular);
		slow_button.Font = new Font("Arial",13,FontStyle.Regular);
		pause_button.Font = new Font("Arial",13,FontStyle.Regular);
		exit_button.Font = new Font("Arial",13,FontStyle.Regular);

		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,100);
		control_panel.Location = new Point (0,800);
		start_button.Location = new Point(20,810);
		fast_button.Location = new Point(80,810);
		slow_button.Location = new Point(140,810);
		pause_button.Location = new Point(180,810);
		exit_button.Location = new Point(240,810);

		AcceptButton = start_button;

		Controls.Add(header_panel);
		header_panel.Controls.Add(title);
		Controls.Add(display_panel);
		Controls.Add(control_panel);
		control_panel.Controls.Add(start_button);
		control_panel.Controls.Add(fast_button);
		control_panel.Controls.Add(slow_button);
		control_panel.Controls.Add(pause_button);
		control_panel.Controls.Add(exit_button);

		CenterToScreen();

		display_panel.Invalidate();
	}

	public class Graphic_Panel : Panel
	{
		private static Pen RedPen    = new Pen(Color.Red, 2);
		private static Pen YellowPen = new Pen(Color.Yellow, 2);
		private static Pen GreenPen  = new Pen(Color.Green, 2);

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graph = e.Graphics;

			graph.DrawEllipse(RedPen,50,20,300,200);
			base.OnPaint(e);
		}
	}
}
