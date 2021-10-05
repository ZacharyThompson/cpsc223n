/*
Zachary Thompson
CPSC 223N
Midterm #1
Oct 4, 2021
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class TravelingBallUI : Form
{
	private Panel header_panel = new Panel();
	private Graphic_Panel display_panel = new Graphic_Panel();
	private Panel control_panel = new Panel();

	private Label title = new Label();
	private Label x_label = new Label();
	private Label y_label = new Label();
	private Label direction_label = new Label();

	private TextBox x_output = new TextBox();
	private TextBox y_output = new TextBox();
	private TextBox direction_output = new TextBox();

	private Button start_button = new Button();
	private Button reset_button = new Button();
	private Button exit_button = new Button();

	private Size max_win_size = new Size(900,900);
	private Size min_win_size = new Size(900,900);

	static private double ball_pos_x;
	static private double ball_pos_y;

	static private bool paused;

	private string direction;

	private double travel_rate = 10.0;

	private System.Timers.Timer animation_timer = new System.Timers.Timer();

	public TravelingBallUI()
	{
		// Window Sizes
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;

		// Text
		Text = "Traveling Ball";
		title.Text = "Traveling Ball by Zachary Thompson";
		start_button.Text = "Start";
		reset_button.Text = "Reset";
		exit_button.Text = "Exit";
		x_label.Text = "X";
		y_label.Text = "Y";
		direction_label.Text = "Direction";

		// Sizes
		Size = MinimumSize;
		header_panel.Size = new Size(900,100);
		display_panel.Size = new Size(900,600);
		control_panel.Size = new Size(900,200);
		start_button.Size = new Size(75,50);
		reset_button.Size = new Size(75,50);
		exit_button.Size = new Size(75,50);
		title.Size = new Size(500, 50);
		x_label.Size = new Size(25,25);
		y_label.Size = new Size(25,25);
		direction_label.Size = new Size(75,25);
		x_output.Size = new Size(50,50);
		y_output.Size = new Size(50,50);
		direction_output.Size = new Size(75,50);

		// Colors
		header_panel.BackColor = Color.LightGreen;
		display_panel.BackColor = Color.LightBlue;
		control_panel.BackColor = Color.Yellow;
		title.ForeColor = Color.Black;
		start_button.BackColor = Color.White;
		reset_button.BackColor = Color.White;
		exit_button.BackColor = Color.White;
		start_button.ForeColor = Color.Black;
		reset_button.ForeColor = Color.Black;
		exit_button.ForeColor = Color.Black;
		x_label.ForeColor = Color.Black;
		y_label.ForeColor = Color.Black;
		direction_label.ForeColor = Color.Black;
		x_output.ForeColor = Color.Black;
		y_output.ForeColor = Color.Black;
		direction_output.ForeColor = Color.Black;
		x_output.BackColor = Color.White;
		y_output.BackColor = Color.White;
		direction_output.BackColor = Color.White;

		// Fonts
		title.Font = new Font("Arial",18,FontStyle.Regular);
		start_button.Font = new Font("Arial",13,FontStyle.Regular);
		reset_button.Font = new Font("Arial",13,FontStyle.Regular);
		exit_button.Font = new Font("Arial",13,FontStyle.Regular);
		x_label.Font = new Font("Arial",13,FontStyle.Regular);
		y_label.Font = new Font("Arial",13,FontStyle.Regular);
		direction_label.Font = new Font("Arial",13,FontStyle.Regular);
		x_output.Font = new Font("Arial",13,FontStyle.Regular);
		y_output.Font = new Font("Arial",13,FontStyle.Regular);
		direction_output.Font = new Font("Arial",13,FontStyle.Regular);

		// Locations
		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,100);
		control_panel.Location = new Point(0,700);
		title.Location = new Point(250,35);
		start_button.Location = new Point(50,100);
		reset_button.Location = new Point(150,100);
		exit_button.Location = new Point(250,100);
		x_label.Location = new Point (600,75);
		y_label.Location = new Point (675,75);
		direction_label.Location = new Point (750,75);
		x_output.Location = new Point(600,100);
		y_output.Location = new Point(675,100);
		direction_output.Location = new Point(750,100);

		// Enter key presses start
		AcceptButton = start_button;

		// Add Controls
		Controls.Add(header_panel);
		header_panel.Controls.Add(title);
		Controls.Add(display_panel);
		Controls.Add(control_panel);
		control_panel.Controls.Add(start_button);
		control_panel.Controls.Add(reset_button);
		control_panel.Controls.Add(exit_button);
		control_panel.Controls.Add(x_label);
		control_panel.Controls.Add(y_label);
		control_panel.Controls.Add(direction_label);
		control_panel.Controls.Add(x_output);
		control_panel.Controls.Add(y_output);
		control_panel.Controls.Add(direction_output);

		//Event Handlers
		start_button.Click += new EventHandler(Start);
		reset_button.Click += new EventHandler(Reset);
		exit_button.Click += new EventHandler(Exit);

		// Initial Ball Position
		ball_pos_x = 800.0;
		ball_pos_y = 0.0;

		// Initial Direction
		direction = "West";

		// Initial Outputs
		x_output.Text = "800";
		y_output.Text = "0";
		direction_output.Text = direction;

		// Setup Timers
		animation_timer.Enabled = false;
		animation_timer.Interval = 34; // 34 approx. equals 30 hz
		animation_timer.Elapsed += new ElapsedEventHandler(Update_Ball_Pos);

		// Start Paused
		paused = true;

		// Causes graphics to be drawn
		display_panel.Invalidate();
	}

	protected void Update_Ball_Pos(Object sender, ElapsedEventArgs events)
	{
		// No Longer Paused
		paused = false;

		int rounded_ball_pos_x = (int)Math.Round(ball_pos_x);
		int rounded_ball_pos_y = (int)Math.Round(ball_pos_y);

		// West
		if (rounded_ball_pos_x >= 0 && rounded_ball_pos_y <= 0)
		{
			ball_pos_x -= travel_rate;
			ball_pos_y = 0.0;
			direction = "West";
		}
		// South
		else if (rounded_ball_pos_x <= 0 && rounded_ball_pos_y <= 500)
		{
			ball_pos_x = 0.0;
			ball_pos_y += travel_rate;
			direction = "South";
		}
		// East
		else if (rounded_ball_pos_x <= 800 && rounded_ball_pos_y >= 500)
		{
			ball_pos_x += travel_rate;
			ball_pos_y = 500.0;
			direction = "East";
		}
		// North
		else if (rounded_ball_pos_x >= 800 && rounded_ball_pos_y >= 0)
		{
			ball_pos_x = 800.0;
			ball_pos_y -= travel_rate;
			direction = "North";
		}


		x_output.Text = rounded_ball_pos_x.ToString();
		y_output.Text = rounded_ball_pos_y.ToString();
		direction_output.Text = direction;

		display_panel.Invalidate();
	}

	protected void Start(Object sender, EventArgs events)
	{
		if (paused)
		{
			animation_timer.Enabled = true;
			start_button.Text = "Pause";
			paused = false;
		}
		else 
		{
			animation_timer.Enabled = false;
			start_button.Text = "Resume";
			paused = true;
		}
	}

	protected void Reset(Object sender, EventArgs events)
	{
		animation_timer.Enabled = false;

		// Initial Ball Position
		ball_pos_x = 800.0;
		ball_pos_y = 0.0;

		// Initial Direction
		direction = "West";

		// Start Paused
		paused = true;

		start_button.Text = "Start";

		// Causes graphics to be drawn
		display_panel.Invalidate();
	}

	protected void Exit(Object sender, EventArgs events)
	{
		animation_timer.Enabled = false;
		Close();
	}

	public class Graphic_Panel : Panel
	{
		private static Brush RedBrush    = new SolidBrush(Color.Red);
		private static Brush OrangeBrush = new SolidBrush(Color.Orange);
		private static Brush YellowBrush = new SolidBrush(Color.Yellow);
		private static Brush GreenBrush  = new SolidBrush(Color.Green);
		private static Pen GrayPen  = new Pen(Color.Gray, 2.0f);

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graph = e.Graphics;

			//Display Ball and Rectangle
			int new_ball_pos_x = (int)Math.Round(ball_pos_x+40);
			int new_ball_pos_y = (int)Math.Round(ball_pos_y+40);
			graph.DrawRectangle(GrayPen,50,50,800,500);
			graph.FillEllipse(OrangeBrush, new_ball_pos_x, new_ball_pos_y, 25, 25);

			if (new_ball_pos_x == 840 && new_ball_pos_y == 40)
			{
				graph.FillEllipse(YellowBrush,450,200,100,100);
			}
			else if (paused)
			{
				graph.FillEllipse(RedBrush,150,200,100,100);
			}
			else
			{
				graph.FillEllipse(GreenBrush,700,200,100,100);
			}

			base.OnPaint(e);
		}
	}
}
