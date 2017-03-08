using IB.DC.Business;
using IB.DC.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IB.DC.Service
{
    public partial class IBService : ServiceBase
    {
        private int eventId;
        Timer timer1;
        string path = "";

        public IBService()
        {
            InitializeComponent();
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            path = (System.IO.Path.GetDirectoryName(executable));
            path = System.IO.Path.Combine(path, "DB");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            bool exists = System.IO.Directory.Exists(path);

            if (!exists)
                System.IO.Directory.CreateDirectory(path);
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("IB DataCollector"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "IB DataCollector", "IB Service");
            }
            eventLog1.Source = "IB DataCollector";
            eventLog1.Log = "IB Service";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Serviço Iniciado.");
            eventId = 0;

            // Set up a timer to trigger every minute.
            timer1 = new Timer(new TimerCallback(timer1_Tick), null, 15000, 60000);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Serviço Parado.");
        }

        private void timer1_Tick(object sender)
        {
            string log="";
            // TODO: Insert monitoring activities here.
            if (Collect()) { 
                eventLog1.WriteEntry("Iniciando coleta de dados. \n" + path, EventLogEntryType.Information, eventId++);
                try
                {
                    var sc = new SpacesController();
                    List<Space> _spaces = new List<Space>();
                    _spaces = sc.ColletarDados(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Endereco"]));
                    if (_spaces.Count>0)
                    {
                        log = "Dados coletados: \n";
                        foreach (Space spc in _spaces)
                        {
                            log += "\n Hora: " + spc.timestamp.ToString() + "\n";
                            log += "Andar: " + spc.andar.ToString() + "\n";
                            log += "Vagas ocupadas: " + spc.vagas_ocupadas.ToString() + "\n";
                            log += "Total de vagas: " + spc.vagas_totais.ToString() + "\n";

                        }
                    }


                    eventLog1.WriteEntry(log, EventLogEntryType.Information, eventId++);
                }
                catch (Exception e)
                {
                    eventLog1.WriteEntry("Erro ao coletar dados: \n" + e.Message, EventLogEntryType.Error, eventId++);
                }
            }
        }

        private bool Collect()
        {
            bool resposta = false;
            int intervalo = 0;
            int minuto = DateTime.Now.Minute;
            intervalo = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["Intervalo"]);
            switch(intervalo)
            {
                case 1:
                    if (minuto == 0)
                    {
                        resposta = true;
                    }
                    break;
                case 2:
                    if (minuto == 0 || minuto==30)
                    {
                        resposta = true;
                    }
                    break;
                case 3:
                    if (minuto == 0 || minuto == 15 || minuto == 30 || minuto == 45)
                    {
                        resposta = true;
                    }
                    break;
                default:
                        resposta = true;
                    break;

            }
            return resposta;
        }
    }
}
