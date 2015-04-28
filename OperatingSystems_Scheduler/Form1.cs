using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OperatingSystems_Scheduler
{
    public partial class CPU_Scheduler : Form
    {
        int numberOfProcesses;
        string type;
        double q;
        Button btn_st;
        TextBox quantum;
        TextBox interrupt;
        TextBox[] names;
        TextBox[] arrivals;
        TextBox[] bursts;
        TextBox[] priorities;
        TextBox newname;        
        TextBox newburst;
        TextBox newpriority;
        Chart timeline;
        int counter;
        Label waiting;
        Scheduler cpu = new Scheduler();
        Stack<Control> objects = new Stack<Control>();
        Scheduler newProcesses = new Scheduler();
        public CPU_Scheduler()
        {
            
            InitializeComponent();
            txt_num.Text = "3";
            cmb_select.SelectedIndex = 0;
            timeline = new Chart();
            waiting = new Label();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                numberOfProcesses = Int32.Parse(txt_num.Text);
                if (numberOfProcesses <= 0)
                    throw new System.Exception("Number of Processes must be positive integer");
                type = cmb_select.SelectedItem.ToString();
                names = new TextBox[numberOfProcesses];
                arrivals = new TextBox[numberOfProcesses];
                bursts = new TextBox[numberOfProcesses];
                priorities = new TextBox[numberOfProcesses];
                quantum = new TextBox();
                interrupt = new TextBox();
                TextBox newname = new TextBox();
                TextBox newburst = new TextBox();
                TextBox newpriority = new TextBox();
                AddNewProcess(numberOfProcesses,type);
                btn_OK.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message +
                    "\n\n (Invalid data please make sure you choosed one of Schedulers and entered a positive number) ");
            }
        }
        
        void AddNewProcess(int n,string t)
        {
            if (n <= 0) throw new System.Exception("Number of Processes must be Positive integer");
            if (t == "Firt Come First Served" || t == "Shortest job first (P)" || t == "Shortest job first (NP)" || t == "Priority (P)" || t == "Priority (NP)" || t == "Round Robin")
            {
                System.Windows.Forms.Label l1 = new System.Windows.Forms.Label();                
                this.Controls.Add(l1);
                l1.Top = 150;
                l1.Left = 100;
                l1.Text = "Name ";
                System.Windows.Forms.Label l2 = new System.Windows.Forms.Label();                
                this.Controls.Add(l2);
                l2.Top =150;
                l2.Left = 250;                
                l2.Text = "Arrival Time";
                System.Windows.Forms.Label l3 = new System.Windows.Forms.Label();                
                this.Controls.Add(l3);
                l3.Top = 150;
                l3.Left = 400;                
                l3.Text = "Duration";
                if (t == "Priority (P)" || t == "Priority (NP)")
                {
                    System.Windows.Forms.Label l4 = new System.Windows.Forms.Label();                    
                    this.Controls.Add(l4);
                    l4.Top = 150;
                    l4.Left = 550;                    
                    l4.Text = "Priority";
                }     
                for (int i = 0; i < n; i++)
                {
                    System.Windows.Forms.TextBox txt1 = new System.Windows.Forms.TextBox();
                    names[i] = txt1;
                    this.Controls.Add(txt1);
                    txt1.Top = (i * 30) + 175;
                    txt1.Left = 100;
                    txt1.Text = "P " + (i+1).ToString();
                    System.Windows.Forms.TextBox txt2 = new System.Windows.Forms.TextBox();
                    arrivals[i] = txt2;
                    this.Controls.Add(txt2);
                    txt2.Top = (i * 30) + 175;
                    txt2.Left = 250;                    
                    txt2.Text = (i + 1).ToString();
                    System.Windows.Forms.TextBox txt3 = new System.Windows.Forms.TextBox();
                    bursts[i] = txt3;
                    this.Controls.Add(txt3);
                    txt3.Top = (i * 30) + 175;
                    txt3.Left = 400;                    
                    txt3.Text =  (i + 1).ToString();
                    if (t == "Priority (P)" || t == "Priority (NP)")
                    {
                        System.Windows.Forms.TextBox txt4 = new System.Windows.Forms.TextBox();
                        priorities[i] = txt4;
                        this.Controls.Add(txt4);
                        txt4.Top = (i * 30) + 175;
                        txt4.Left = 550;                        
                        txt4.Text = (i + 1).ToString();
                    }              
                    
                }
                if (t == "Round Robin")
                {
                    System.Windows.Forms.Label l5 = new System.Windows.Forms.Label();                    
                    this.Controls.Add(l5);
                    l5.Top = (numberOfProcesses * 30) + 175;
                    l5.Left = 50;
                    l5.Text = "Quantum Time ";
                    System.Windows.Forms.TextBox txt5 = new System.Windows.Forms.TextBox();
                    quantum = txt5;
                    this.Controls.Add(txt5);
                    txt5.Top = (numberOfProcesses * 30) + 175;
                    txt5.Left = 150;
                    txt5.Text = "1" ;                   
                }
                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "btn_start";
                btn.Font = new System.Drawing.Font("Century", 15, System.Drawing.FontStyle.Regular);
                btn.ForeColor = System.Drawing.Color.Maroon;
                this.Controls.Add(btn);
                btn.Top = (n * 30) + 200;
                btn.Left = 300;
                btn.Size= new System.Drawing.Size(500,30);
                btn.Text = "START ";
                btn_st = btn;
                btn.Click += new EventHandler(Button_Click_st);
                System.Windows.Forms.Button btn1 = new System.Windows.Forms.Button();
                btn1.Name = "btn_Restart";
                btn1.Font = new System.Drawing.Font("Century", 15, System.Drawing.FontStyle.Regular);
                btn1.ForeColor = System.Drawing.Color.Maroon;
                this.Controls.Add(btn1);
                btn1.Top = (n * 30) + 200;
                btn1.Left = 850;
                btn1.Size = new System.Drawing.Size(130, 30);
                btn1.Text = "RESTART ";                
                btn1.Click += new EventHandler(Button_Click_res);
            }
            else throw new System.Exception("Type of Scheduler is invalid");
        }

        private void Button_Click_st(object sender, EventArgs e)
        {
            try
            {
                if (type == "Round Robin")
                {
                    q = Convert.ToDouble(quantum.Text);
                    if (q <= 0)
                        throw new System.Exception("Quantum must be positive integer");
                }
                fillScheduler();
                scheduleCPU();
                btn_st.Enabled = false;
                System.Windows.Forms.Button btn3 = new System.Windows.Forms.Button();                
                btn3.Font = new System.Drawing.Font("Century", 15, System.Drawing.FontStyle.Regular);
                btn3.ForeColor = System.Drawing.Color.Maroon;
                this.Controls.Add(btn3);
                btn3.Top = (numberOfProcesses * 30) + 450;
                btn3.Left = 850;
                btn3.Size = new System.Drawing.Size(130, 30);
                btn3.Text = "INTRRUPT ";
                btn3.Click += new EventHandler(Button_Click_int);
                System.Windows.Forms.Button b2 = new System.Windows.Forms.Button();
                b2.Font = new System.Drawing.Font("Century", 15, System.Drawing.FontStyle.Regular);
                b2.ForeColor = System.Drawing.Color.Maroon;
                b2.Size = new System.Drawing.Size(130, 30);
                this.Controls.Add(b2);                
                b2.Top = (numberOfProcesses * 30) + 450;
                b2.Left = 700;
                b2.Text = "RESET ";
                b2.Click += new EventHandler(Button_Click_con);
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void FCFS(Scheduler s)
        {
            createWaiting();
            createTimeline();
            Scheduler temp = new Scheduler();           
            temp = s.sort("arrival");
            node p = temp.getProcesses().getChain();
            double i = p.getData().getArrival();
            int counter = 0;
            double average = 0;
            Series idle = new Series();
            createSeries(idle, i);
            while (p != null)
            {
                Series barSeries = new Series();
                barSeries.ChartType = SeriesChartType.StackedBar;
                barSeries.XValueType = ChartValueType.String;                
                barSeries.Points.AddXY("timeline", p.getData().getDuration());
                barSeries.Label = p.getData().getName();            
                timeline.Series.Add(barSeries);
                average = average + i- p.getData().getArrival();
                i = i + p.getData().getDuration();
                counter++;
                p = p.getNext();
            }
            waiting.Text = " Average Waiting Time = " + average.ToString() + " / " + counter.ToString() + " = " + (average / counter).ToString();
            cpu.clear();
        }

        void SJF_NPrem(Scheduler s)
        {
            createWaiting();
            createTimeline();                  
            Scheduler temp = new Scheduler();
            temp = s.sort("arrival");
            double first = temp.getProcesses().getChain().getData().getArrival();
            double i = temp.getProcesses().getChain().getData().getArrival();
            int counter = 0;
            double average = 0;
            Series idle = new Series();
            createSeries(idle, i);
            while(temp.getProcesses().getChain() != null)
	        {
		        Scheduler temp2 = new Scheduler();
		        while(temp.getProcesses().getChain()!=null && temp.getProcesses().getChain().getData().getArrival() <= first)
		        {			
			        temp2.add(temp.getProcesses().getChain().getData());
			        temp.remove(temp.getProcesses().getChain().getData().getName());
		        }
		        temp2 = temp2.sort("duration");
		        node p2=temp2.getProcesses().getChain();
		        while(p2!=null)
		        {
			        Series barSeries = new Series();
                    barSeries.ChartType = SeriesChartType.StackedBar;
                    barSeries.XValueType = ChartValueType.String;                    
                    barSeries.Points.AddXY("timeline", p2.getData().getDuration());
                    barSeries.Label = p2.getData().getName();
                    timeline.Series.Add(barSeries);
			        average = average + i - p2.getData().getArrival();
			        i=i+p2.getData().getDuration();
			        counter++;
			        p2=p2.getNext();
		        }
	            first = i;
                temp2.clear();
	        }
            waiting.Text = " Average Waiting Time = " + average.ToString() + " / " + counter.ToString() + " = " + (average / counter).ToString();
            temp.clear();
            cpu.clear();
        }

        void Priority_NPrem(Scheduler s)
        {
            createWaiting();
            createTimeline();  
            Scheduler temp = new Scheduler();
            temp = s.sort("arrival");
            double first = temp.getProcesses().getChain().getData().getArrival();
            double i = temp.getProcesses().getChain().getData().getArrival();
            int counter = 0;
            double average = 0;
            Series idle = new Series();
            createSeries(idle, i);
            while(temp.getProcesses().getChain() != null)
	        {
		        Scheduler temp2 = new Scheduler();
		        while(temp.getProcesses().getChain()!=null && temp.getProcesses().getChain().getData().getArrival() <= first)
		        {			
			        temp2.add(temp.getProcesses().getChain().getData());
			        temp.remove(temp.getProcesses().getChain().getData().getName());
		        }
                temp2 = temp2.sort("priority");
		        node p2=temp2.getProcesses().getChain();
		        while(p2!=null)
		        {
			        Series barSeries = new Series();
                    barSeries.ChartType = SeriesChartType.StackedBar;
                    barSeries.XValueType = ChartValueType.String;                    
                    barSeries.Points.AddXY("timeline", p2.getData().getDuration());
                    barSeries.Label = p2.getData().getName();
                    timeline.Series.Add(barSeries);
			        average = average + i - p2.getData().getArrival();
			        i=i+p2.getData().getDuration();
			        counter++;
			        p2=p2.getNext();
		        }
	            first = i;
                temp2.clear();
	        }
            waiting.Text = " Average Waiting Time = " + average.ToString() + " / " + counter.ToString() + " = " + (average / counter).ToString();
            temp.clear();
            cpu.clear();
        }


        void SJF_Prem(Scheduler s)
        {
            createWaiting();
            createTimeline();
            Scheduler temp = new Scheduler();
            temp = s.sort("arrival");
            double i = temp.getProcesses().getChain().getData().getArrival();
            int counter = 0;
            double average = 0;
            double working = 0;
            Series idle = new Series();
            createSeries(idle, i);
            while(temp.getProcesses().getChain() != null)
	        {
		        Scheduler temp2 = new Scheduler();
		        double first = temp.getProcesses().getChain().getData().getArrival();
		        while(temp.getProcesses().getChain()!=null && temp.getProcesses().getChain().getData().getArrival() <= first)
		        {			
			        temp2.add(temp.getProcesses().getChain().getData());
			        temp.remove(temp.getProcesses().getChain().getData().getName());
		        }
		        temp2 = temp2.sort("duration");
		        node p2=temp2.getProcesses().getChain();
		        while(p2!=null)
		        {
                    if (temp.getProcesses().getChain() != null && p2.getData().getDuration() + p2.getData().getArrival() > temp.getProcesses().getChain().getData().getArrival())
			        {
				        Series barSeries = new Series();
                        barSeries.ChartType = SeriesChartType.StackedBar;
                        barSeries.XValueType = ChartValueType.String;                        
                        barSeries.Points.AddXY("timeline", temp.getProcesses().getChain().getData().getArrival() - p2.getData().getArrival()); 
                        barSeries.Color = p2.getData().getColor();
                        barSeries.Label = p2.getData().getName();
                        timeline.Series.Add(barSeries);
				        p2.getData().setDuration(p2.getData().getDuration()+p2.getData().getArrival()-temp.getProcesses().getChain().getData().getArrival());
				        temp.add(p2.getData());
				        i=temp.getProcesses().getChain().getData().getArrival();
				        working = working  + temp.getProcesses().getChain().getData().getArrival() - p2.getData().getArrival();
			        }
			        else
			        {
				        Series barSeries = new Series();
                        barSeries.ChartType = SeriesChartType.StackedBar;
                        barSeries.XValueType = ChartValueType.String;                        
                        barSeries.Points.AddXY("timeline", p2.getData().getDuration());                        
                        barSeries.Color = p2.getData().getColor();
                        barSeries.Label = p2.getData().getName();
                        timeline.Series.Add(barSeries);
				        average = average + i - p2.getData().getArrival();
				        i=i+p2.getData().getDuration();
				        counter++;
			        }
			        p2=p2.getNext();
		        }
                temp2.clear();
	        }
            waiting.Text = " Average Waiting Time = " + (average-working).ToString() +  " / " + counter.ToString() + " = " + ((average - working) / counter).ToString();
            temp.clear();
            cpu.clear();
        }

        void Priority_Prem(Scheduler s)
        {
            createWaiting();
            createTimeline(); 
            Scheduler temp = new Scheduler();
            temp = s.sort("arrival");
            double i = temp.getProcesses().getChain().getData().getArrival();
            int counter = 0;
            double average = 0;
            double working = 0;
            Series idle = new Series();
            createSeries(idle, i);
            while (temp.getProcesses().getChain() != null)
            {
                Scheduler temp2 = new Scheduler();
                double first = temp.getProcesses().getChain().getData().getArrival();
                while (temp.getProcesses().getChain() != null && temp.getProcesses().getChain().getData().getArrival() <= first)
                {
                    temp2.add(temp.getProcesses().getChain().getData());
                    temp.remove(temp.getProcesses().getChain().getData().getName());
                }
                temp2 = temp2.sort("priority");
                node p2 = temp2.getProcesses().getChain();
                while (p2 != null)
                {
                    if (temp.getProcesses().getChain() != null && p2.getData().getDuration() + p2.getData().getArrival() > temp.getProcesses().getChain().getData().getArrival())
                    {
                        Series barSeries = new Series();
                        barSeries.ChartType = SeriesChartType.StackedBar;
                        barSeries.XValueType = ChartValueType.String;                        
                        barSeries.Points.AddXY("timeline", temp.getProcesses().getChain().getData().getArrival() - p2.getData().getArrival());
                        timeline.Series.Add(barSeries);
                        barSeries.Color = p2.getData().getColor();
                        barSeries.Label = p2.getData().getName();
                        p2.getData().setDuration(p2.getData().getDuration() + p2.getData().getArrival() - temp.getProcesses().getChain().getData().getArrival());
                        temp.add(p2.getData());
                        i = temp.getProcesses().getChain().getData().getArrival();
                        working = working + temp.getProcesses().getChain().getData().getArrival() - p2.getData().getArrival();
                    }
                    else
                    {
                        Series barSeries = new Series();
                        barSeries.ChartType = SeriesChartType.StackedBar;
                        barSeries.XValueType = ChartValueType.String;                       
                        barSeries.Points.AddXY("timeline", p2.getData().getDuration());
                        barSeries.Color = p2.getData().getColor();
                        barSeries.Label = p2.getData().getName();
                        timeline.Series.Add(barSeries);
                        average = average + i - p2.getData().getArrival();
                        i = i + p2.getData().getDuration();
                        counter++;
                    }
                    p2 = p2.getNext();
                }
            }
            waiting.Text = " Average Waiting Time = " + (average - working).ToString() + " / " + counter.ToString() + " = " + ((average - working) / counter).ToString();
            cpu.clear();
        }

        void RoundRobin(Scheduler s,double q)
        {
            
            createWaiting();            
            createTimeline();   
            Scheduler temp = new Scheduler();
            temp = s.sort("arrival");
            node p = temp.getProcesses().getChain();
            double first = p.getData().getArrival();
            double i = p.getData().getArrival();
            double j = 0;
            int counter = 0;
            double average = 0;
            double working = 0;
            double arrival = 0;
            Series idle = new Series();
            createSeries(idle, i);            
            node p1 = temp.getProcesses().getChain();
            while (p1 != null)
            {
                arrival = arrival + p1.getData().getArrival();
                p1 = p1.getNext();
            }
            while(temp.getProcesses().getChain()!=null)
	        {
		        j=(temp.getProcesses().getChain().getData().getDuration() <= q)? temp.getProcesses().getChain().getData().getDuration(): q;
		        Series barSeries = new Series();
                barSeries.ChartType = SeriesChartType.StackedBar;
                barSeries.XValueType = ChartValueType.String;                
                barSeries.Points.AddXY("timeline", j); 
                barSeries.Color = temp.getProcesses().getChain().getData().getColor();
                barSeries.Label = temp.getProcesses().getChain().getData().getName();   
                timeline.Series.Add(barSeries);
		        if(temp.getProcesses().getChain().getData().getDuration() <= q)
		        {
                    average = average + i;
			        i = i + temp.getProcesses().getChain().getData().getDuration();
			        counter++;
		        }
		        else
		        {
			        i = i + q;
			        working = working + q;
			        temp.getProcesses().getChain().getData().setDuration(temp.getProcesses().getChain().getData().getDuration() - q);                    
                    temp.getProcesses().getChain().getData().setArrival(i);
			        temp.add(temp.getProcesses().getChain().getData());
		        }
		        temp.remove(temp.getProcesses().getChain().getData().getName());
                temp = temp.sort("arrival");
                
	        }
	        waiting.Text = " Average Waiting Time = ( "+(average-arrival).ToString()+" - "+working.ToString()+" ) / "+counter.ToString()+" = "+((average-working)/counter).ToString();
            cpu.clear();
        }

        private void Button_Click_res(object sender, EventArgs e)
        {
            this.Controls.Clear();
            cpu.clear();
            newProcesses.clear();
            timeline = new Chart();
            waiting = new Label();
            this.InitializeComponent();
            txt_num.Text = "3";
            cmb_select.SelectedIndex = 0;
        }

        void createTimeline()
        {
            timeline.Size = new System.Drawing.Size(1200, 100);
            ChartArea area = new ChartArea(counter.ToString());
            counter++;
            timeline.ChartAreas.Add(area);
            timeline.Top = 325 + (30 * numberOfProcesses);            
            this.Controls.Add(timeline);
        }

        void createWaiting()
        {
            waiting.Font = new System.Drawing.Font("Century", 12, System.Drawing.FontStyle.Regular);
            waiting.ForeColor = System.Drawing.Color.Maroon;
            waiting.Top = 275 + (30 * numberOfProcesses);
            waiting.Size = new System.Drawing.Size(500, 20);
            this.Controls.Add(waiting);
        }

        void createSeries(Series idle, double i)
        {
            idle.ChartType = SeriesChartType.StackedBar;
            idle.XValueType = ChartValueType.String;
            idle.Points.AddXY("timeline", i);
            idle.Label = "Idle";
            idle.Color = System.Drawing.Color.White;
            timeline.Series.Add(idle);
        }

        private void Button_Click_int(object sender, EventArgs e)
        {
            this.Controls.Remove(timeline);
            this.Controls.Remove(waiting);
            System.Windows.Forms.Label l = new System.Windows.Forms.Label();
            this.Controls.Add(l);
            objects.Push(l);
            l.Top = 500 + (30 * numberOfProcesses);
            l.Left = 100;            
            l.Text = "Time of interruption: ";
            l.Size = new System.Drawing.Size(150, 30);
            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();            
            this.Controls.Add(txt);
            objects.Push(txt);
            txt.Top = (numberOfProcesses * 30) + 500;
            txt.Left = 250;
            interrupt = txt;
            txt.Text = "3";
            System.Windows.Forms.Button b1 = new System.Windows.Forms.Button();            
            b1.ForeColor = System.Drawing.Color.Maroon;
            this.Controls.Add(b1);
            objects.Push(b1);
            b1.Top = (numberOfProcesses * 30) + 500;
            b1.Left = 400;
            b1.Size = new System.Drawing.Size(150, 30);
            b1.Text = "ADD Process";
            b1.Click += new EventHandler(Button_Click_add);            
        }

        private void Button_Click_add(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox t1 = new System.Windows.Forms.TextBox();            
            this.Controls.Add(t1);
            objects.Push(t1);
            t1.Top = (numberOfProcesses * 30) + 550;
            t1.Left = 100;
            t1.Text = "New ";
            newname = t1;
            System.Windows.Forms.TextBox t2 = new System.Windows.Forms.TextBox();
            this.Controls.Add(t2);
            objects.Push(t2);
            t2.Top = (numberOfProcesses * 30) + 550;
            t2.Left = 250;
            t2.Text = interrupt.Text;
            t2.Enabled = false;
            System.Windows.Forms.TextBox t3 = new System.Windows.Forms.TextBox();            
            this.Controls.Add(t3);
            objects.Push(t3);
            t3.Top = (numberOfProcesses * 30) + 550;
            t3.Left = 400;
            t3.Text = "5";
            newburst = t3;
            System.Windows.Forms.Button bt = new System.Windows.Forms.Button();
            bt.ForeColor = System.Drawing.Color.Maroon;
            bt.Size = new System.Drawing.Size(150, 30);
            this.Controls.Add(bt);
            objects.Push(bt);
            bt.Top = (numberOfProcesses * 30) + 550;
            bt.Left = 550;
            bt.Text = "OK ";
            bt.Click += new EventHandler(Button_Click_new);
            if (type == "Priority (P)" || type == "Priority (NP)")
            {
                System.Windows.Forms.TextBox t4 = new System.Windows.Forms.TextBox();                
                this.Controls.Add(t4);
                objects.Push(t4);
                t4.Top = (numberOfProcesses * 30) + 550;
                t4.Left = 550;
                t4.Text = "1";
                newpriority = t4;
                bt.Left = 700;
            }            
        }

        private void Button_Click_con(object sender, EventArgs e)
        {
            this.Controls.Remove(timeline);
            this.Controls.Remove(waiting);
            timeline = new Chart();
            waiting = new Label();
            cpu.clear();
            fillScheduler();            
            while (objects.Count > 0)
                this.Controls.Remove(objects.Pop());
            objects.Clear();
            scheduleCPU();
        }

        private void Button_Click_new(object sender, EventArgs e)
        {
            try
            {
                timeline = new Chart();
                waiting = new Label();
                Process Pn = new Process();
                Pn.setName(newname.Text);
                Pn.setArrival(Convert.ToDouble(interrupt.Text));
                if (Pn.getArrival() <= 0)
                    throw new System.Exception("New Process arrival time must be positive integer");
                Pn.setDuration(Convert.ToDouble(newburst.Text));
                if (Pn.getDuration() <= 0)
                    throw new System.Exception(" New Process Burst time must be positive integer");
                Random rnd = new Random(18*System.Environment.TickCount);
                Pn.setColor(System.Drawing.Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255)));
                if (type == "Priority (P)" || type == "Priority (NP)")
                {
                    Pn.setPriority(Int32.Parse(newpriority.Text));
                    if (Pn.getPriority() <= 0)
                        throw new System.Exception("New Process priority must be positive integer");
                }                
                fillScheduler();
                node p = new node();
                p = newProcesses.getProcesses().getChain();
                while (p != null)
                {
                    cpu.add(p.getData());
                    p = p.getNext();
                }
                cpu.add(Pn);
                newProcesses.add(Pn);
                while (objects.Count > 0)
                    this.Controls.Remove(objects.Pop());
                objects.Clear();
                scheduleCPU();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void scheduleCPU()
        {
            if (type == "Firt Come First Served")
                FCFS(cpu);
            else if (type == "Shortest job first (NP)")
                SJF_NPrem(cpu);
            else if (type == "Priority (NP)")
                Priority_NPrem(cpu);
            else if (type == "Shortest job first (P)")
                SJF_Prem(cpu);
            else if (type == "Priority (P)")
                Priority_Prem(cpu);
            else if (type == "Round Robin")
            {       
                RoundRobin(cpu, q);
            }
        }

        void fillScheduler()
        {
            for (int i = 0; i < numberOfProcesses; i++)
            {
                Process p = new Process();
                p.setName(names[i].Text);
                p.setArrival(Convert.ToDouble(arrivals[i].Text));
                if (p.getArrival() <= 0)
                    throw new System.Exception("Process " + (i + 1).ToString() + " arrival time must be positive integer");
                p.setDuration(Convert.ToDouble(bursts[i].Text));
                if (p.getDuration() <= 0)
                    throw new System.Exception("Process " + (i + 1).ToString() + " Burst time must be positive integer");
                Random rnd = new Random(i * (System.Environment.TickCount));
                p.setColor(System.Drawing.Color.FromArgb(255, rnd.Next(255), rnd.Next(255), rnd.Next(255)));
                if (type == "Priority (P)" || type == "Priority (NP)")
                {
                    p.setPriority(Int32.Parse(priorities[i].Text));
                    if (p.getPriority() <= 0)
                        throw new System.Exception("Process " + (i + 1).ToString() + " priority must be positive integer");
                }
                cpu.add(p);
            }
        }
    }
}
