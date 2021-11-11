// Zachary Thompson
// Midterm #2
// November 8, 2021
// CPSC 223N-1

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class RicochetUI : Form
{
	private Panel header_panel = new Panel();
	private Graphic_Panel display_panel = new Graphic_Panel();
	private Panel control_panel = new Panel();

	private Label title = new Label();
	private Label coordinates_label = new Label();
	private Label x_label = new Label();
	private Label y_label = new Label();
	private Label direction_label = new Label();
	private Label speed_label = new Label();
	private Label current_speed_label = new Label();

	private TextBox x_output = new TextBox();
	private TextBox y_output = new TextBox();
	private TextBox direction_input = new TextBox();
	private TextBox speed_input = new TextBox();
	private TextBox current_speed_output = new TextBox();

	private Button start_button = new Button();
	private Button reset_button = new Button();
	private Button exit_button = new Button();
	private Button plus_button = new Button();
	private Button minus_button = new Button();

	private Size max_win_size = new Size(800,900);
	private Size min_win_size = new Size(800,900);

	static private bool paused;

	static private double ball_radius = 10;
	private double ball_speed; // pix/tick
	private double ball_direction; // degrees
	static private double ball_pos_x;
	static private double ball_pos_y;
	static private bool ball_visible;
	static private bool in_progress;

	private System.Timers.Timer animation_timer = new System.Timers.Timer();
	
	public RicochetUI()
	{
		// Window Sizes
		MaximumSize = max_win_size;
		MinimumSize = min_win_size;

		// Text
		Text = "Ricochet Ball";
		title.Text = "Ricochet Ball by Zachary Thompson";
		start_button.Text = "Start";
		reset_button.Text = "Reset";
		exit_button.Text = "Exit";
		coordinates_label.Text = "Coordinates of center of ball";
		x_label.Text = "X =";
		y_label.Text = "Y =";
		direction_label.Text = "Enter Direction (degrees)";
		speed_label.Text = "Enter Speed (pixel/second)";
		speed_input.Text = "80";
		direction_input.Text = "";
		current_speed_output.Text = "80";
		current_speed_label.Text = "Current Speed";
		plus_button.Text = "+";
		minus_button.Text = "-";

		// Sizes
		Size = MinimumSize;
		header_panel.Size = new Size(800,100);
		display_panel.Size = new Size(800,600);
		control_panel.Size = new Size(800,200);
		start_button.Size = new Size(75,50);
		reset_button.Size = new Size(75,50);
		exit_button.Size = new Size(75,50);
		title.Size = new Size(500, 50);
		x_label.Size = new Size(30,25);
		y_label.Size = new Size(30,25);
		direction_label.Size = new Size(200,35);
		speed_label.Size = new Size(225,25);
		x_output.Size = new Size(50,50);
		y_output.Size = new Size(50,50);
		direction_input.Size = new Size(50,50);
		speed_input.Size = new Size(50,50);
		current_speed_output.Size = new Size(125,75);
		current_speed_label.Size = new Size(150,50);
		plus_button.Size = new Size(30,30);
		minus_button.Size = new Size(30,30);

		// Colors
		header_panel.BackColor = Color.LightBlue;
		display_panel.BackColor = Color.Green;
		control_panel.BackColor = Color.Yellow;
		start_button.BackColor = Color.White;
		reset_button.BackColor = Color.White;
		exit_button.BackColor = Color.White;
		start_button.ForeColor = Color.Black;
		reset_button.ForeColor = Color.Black;
		exit_button.ForeColor = Color.Black;
		x_label.ForeColor = Color.Black;
		y_label.ForeColor = Color.Black;
		direction_label.ForeColor = Color.Black;
		speed_label.ForeColor = Color.Black;
		x_output.ForeColor = Color.Black;
		y_output.ForeColor = Color.Black;
		direction_input.ForeColor = Color.Black;
		speed_input.ForeColor = Color.Black;
		x_output.BackColor = Color.White;
		y_output.BackColor = Color.White;
		direction_input.BackColor = Color.White;
		speed_input.BackColor = Color.White;
		current_speed_output.BackColor = Color.Purple;
		current_speed_output.ForeColor = Color.Black;
		current_speed_label.ForeColor = Color.Black;
		plus_button.BackColor = Color.Purple;
		plus_button.ForeColor = Color.Black;
		minus_button.BackColor = Color.Purple;
		minus_button.ForeColor = Color.Black;
		

		// Fonts
		title.Font = new Font("Arial",18,FontStyle.Regular);
		start_button.Font = new Font("Arial",13,FontStyle.Regular);
		reset_button.Font = new Font("Arial",13,FontStyle.Regular);
		exit_button.Font = new Font("Arial",13,FontStyle.Regular);
		x_label.Font = new Font("Arial",13,FontStyle.Regular);
		y_label.Font = new Font("Arial",13,FontStyle.Regular);
		direction_label.Font = new Font("Arial",13,FontStyle.Regular);
		speed_label.Font = new Font("Arial",13,FontStyle.Regular);
		x_output.Font = new Font("Arial",13,FontStyle.Regular);
		y_output.Font = new Font("Arial",13,FontStyle.Regular);
		direction_input.Font = new Font("Arial",13,FontStyle.Regular);
		speed_input.Font = new Font("Arial",13,FontStyle.Regular);
		current_speed_output.Font = new Font("Arial",16,FontStyle.Regular);
		current_speed_label.Font = new Font("Arial",13,FontStyle.Regular);
		plus_button.Font = new Font("Arial",13,FontStyle.Regular);
		minus_button.Font = new Font("Arial",13,FontStyle.Regular);

		// Locations
		header_panel.Location = new Point(0,0);
		display_panel.Location = new Point(0,100);
		control_panel.Location = new Point(0,700);
		title.Location = new Point(200,35);
		start_button.Location = new Point(50,100);
		reset_button.Location = new Point(50,30);
		exit_button.Location = new Point(710,100);
		speed_label.Location = new Point(200,30);
		speed_input.Location = new Point(425,30);
		direction_label.Location = new Point(500,30);
		direction_input.Location = new Point(700,30);
		x_label.Location = new Point(400,100);
		x_output.Location = new Point(445,100);
		y_label.Location = new Point(510,100);
		y_output.Location = new Point(555,100);
		plus_button.Location = new Point(150,50);
		minus_button.Location = new Point(150,100);
		current_speed_output.Location = new Point(200,100);
		current_speed_label.Location = new Point(200,75);

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
		control_panel.Controls.Add(speed_label);
		control_panel.Controls.Add(speed_input);
		control_panel.Controls.Add(direction_label);
		control_panel.Controls.Add(direction_input);
		control_panel.Controls.Add(x_label);
		control_panel.Controls.Add(y_label);
		control_panel.Controls.Add(x_output);
		control_panel.Controls.Add(y_output);
		control_panel.Controls.Add(current_speed_output);
		control_panel.Controls.Add(current_speed_label);
		control_panel.Controls.Add(plus_button);
		control_panel.Controls.Add(minus_button);

		// Event Handlers
		start_button.Click += new EventHandler(Start);
		reset_button.Click += new EventHandler(Reset);
		exit_button.Click += new EventHandler(Exit);
		plus_button.Click += new EventHandler(IncreaseSpeed);
		minus_button.Click += new EventHandler(DecreaseSpeed);

		// Setup Timer
		animation_timer.Enabled = false;
		animation_timer.Interval = (int)Math.Round(1000.0/30.0); // 34 approx. equals 30 hz
		animation_timer.Elapsed += new ElapsedEventHandler(MoveBall);

		// Initial Values
		paused = true;
		ball_speed = 80 * animation_timer.Interval / 1000;
		ball_direction = 0.0;
		ball_pos_x = 400;
		ball_pos_y = 300;
		ball_visible = false;
		in_progress = false;

		// For test #2 the speed input box is no longer needed
		speed_input.Enabled = false;

		display_panel.Invalidate();
	}

	protected void IncreaseSpeed(Object sender, EventArgs events)
	{
		if (!paused)
		{
			ball_speed += 10 * animation_timer.Interval / 1000;
			// Cap speed to 360 pix/sec
			if (ball_speed > 360 * animation_timer.Interval / 1000)
				ball_speed = 360 * animation_timer.Interval / 1000;
			current_speed_output.Text = Math.Round(ball_speed / animation_timer.Interval * 1000, 2).ToString();
		}
	}

	protected void DecreaseSpeed(Object sender, EventArgs events)
	{
		if (!paused)
		{
			ball_speed -= 10 * animation_timer.Interval / 1000;
			// Min. speed is 0 pix/sec
			if (ball_speed < 0)
				ball_speed = 0;
			current_speed_output.Text = Math.Round(ball_speed / animation_timer.Interval * 1000, 2).ToString();
		}
	}

	private double DegToRad(double angle)
	{
		return angle * (Math.PI/180);
	}

	protected void MoveBall(Object sender, ElapsedEventArgs events)
	{
		ball_pos_x = ball_pos_x + ball_speed * Math.Cos(DegToRad(ball_direction));
		ball_pos_y = ball_pos_y + ball_speed * Math.Sin(DegToRad(ball_direction));
		DetectCollision();
		x_output.Text = Math.Round(ball_pos_x + ball_radius).ToString();
		y_output.Text = Math.Round(ball_pos_y + ball_radius).ToString();
		display_panel.Invalidate();
	}


	private void DetectCollision()
	{
		// Left
		if (ball_pos_x < 0)
		{
			ball_direction = 180-ball_direction;
			ball_pos_x = 0;
		}
		// Right
		if (ball_pos_x + 2*ball_radius > 800)
		{
			ball_direction = 180-ball_direction;
			ball_pos_x = 800 - 2*ball_radius;
		}
		// Up
		if (ball_pos_y < 0)
		{
			ball_direction = -ball_direction;
			ball_pos_y = 0;
		}
		// Down
		if (ball_pos_y + 2*ball_radius > 600)
		{
			ball_direction = -ball_direction;
			ball_pos_y = 600 - 2*ball_radius;
		}


		// Update direction to be between 0-360
		if (ball_direction < 0)
		{
			ball_direction += 360;
		}
		else if (ball_direction > 360)
		{
			ball_direction -= 360;
		}
	}

	private bool CheckInput()
	{
		if (direction_input.Text == "")
		{
			return false;
		}

		try
		{
			if (!in_progress)
			{
				// Convert from pix/sec to pix/tick
				/* ball_speed = Convert.ToDouble(speed_input.Text) * animation_timer.Interval / 1000; */
				ball_direction = Convert.ToDouble(direction_input.Text);
			}
		}
		catch
		{
			return false;
		}

		/*
		if (ball_speed < 0)
		{
			ball_speed = 0;
			return false;
		}
		*/

		return true;
	}

	protected void Start(Object sender, EventArgs events)
	{
		if (CheckInput())
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
				start_button.Text = "Unpause";
				paused = true;
			}
			ball_visible = true;
			in_progress = true;
			current_speed_output.Text = Math.Round(ball_speed / animation_timer.Interval * 1000, 2).ToString();
		}
	}

	protected void Reset(Object sender, EventArgs events)
	{
		// Initial Values
		paused = true;
		ball_speed = 80 * animation_timer.Interval / 1000;
		ball_direction = 0.0;
		ball_pos_x = 400;
		ball_pos_y = 300;
		ball_visible = false;
		in_progress = false;

		// Reset Timer
		animation_timer.Enabled = false;

		// Reset Text
		start_button.Text = "Start";
		x_output.Text = "";
		y_output.Text = "";
		/* speed_input.Text = ""; */
		direction_input.Text = "";
		current_speed_output.Text = "80";

		display_panel.Invalidate();
	}

	protected void Exit(Object sender, EventArgs events)
	{
		Close();
	}

	public class Graphic_Panel : Panel
	{
		private static Brush RedBrush = new SolidBrush(Color.Red);

		protected override void OnPaint(PaintEventArgs e)
		{
			int new_ball_pos_x = (int)Math.Round(ball_pos_x);
			int new_ball_pos_y = (int)Math.Round(ball_pos_y);
			int new_ball_radius = (int)Math.Round(ball_radius);
			Graphics graph = e.Graphics;
			if (ball_visible)
			{
				graph.FillEllipse(RedBrush,new_ball_pos_x,new_ball_pos_y,2*new_ball_radius,2*new_ball_radius);
			}
			base.OnPaint(e);
		}
	}
}
