using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class StopLightUI : Form
{
	private Panel header_panel;
	private Form display_form;
	private Panel control_panel;

	private Label title;

	private Button start_button;
	private Button fast_button;
	private Button slow_button;
	private Button pause_button;
	private Button exit_button;

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

		title.Font = new Font("Arial",26,FontStyle.Regular);
		start_button.Font = new Font("Arial",26,FontStyle.Regular);
		fast_button.Font = new Font("Arial",26,FontStyle.Regular);
		slow_button.Font = new Font("Arial",26,FontStyle.Regular);
		pause_button.Font = new Font("Arial",26,FontStyle.Regular);
		exit_button.Font = new Font("Arial",26,FontStyle.Regular);

		header_panel.Location = new Point(0,0);
		display_form.Location = new Point(0,100);
		control_panel.Location = new Point (0,800);

	}
}
