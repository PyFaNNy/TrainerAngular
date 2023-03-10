using MediatR;
using Microsoft.AspNetCore.SignalR;
using Trainer.Application.Aggregates.Examination.Commands.FinishExamination;
using Trainer.Application.Aggregates.Examination.Queries.GetExamination;
using Trainer.Application.Aggregates.Results.Commands.Create;

namespace Trainer.Chart
{
    public class ChartHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly Random _rnd = new Random();
        public ChartHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task ProvideReading(int statusTonometr, int statusTermometr, int statusHeartrate, int statusOximetr,
            int sensor1, int sensor2, int sensor3, int sensor4,
            bool tonometrOn, bool termometrOn, bool heartrateOn, bool oximetrOn, Guid id)
        {
            bool flag = true;
            var examination = await _mediator.Send(new GetExaminationQuery { ExaminationId = id});
            CountIndicators(examination);

            if (examination.DiaSis && (sensor1 != 1 || !tonometrOn || statusTonometr != 0))
            {
                await Clients.Caller.SendAsync("error", "Не правильно подключен тонометр");
                flag = false;
            }
            if (examination.Tempareture && (sensor2 != 2 || !termometrOn || statusTermometr != 0))
            {
                await Clients.Caller.SendAsync("error", "Не правильно подключен термометр");
                flag = false;
            }
            if (examination.HeartRate && (sensor3 != 3 || !heartrateOn || statusHeartrate != 0))
            {
                await Clients.Caller.SendAsync("error", "Не правильно подключен пульсометр");
                flag = false;
            }
            if (examination.SpO2 && (sensor4 != 4 || !oximetrOn || statusOximetr != 0))
            {
                await Clients.Caller.SendAsync("error", "Не правильно подключен оксиметр");
                flag = false;
            }

            if (flag)
            {
                uint time = 0;
                int sis = 180;
                int dia = 140;
                int heartRate = 0, averageHeartRate = 0;
                int sep = 0, avarageSep = 0;
                double temperature = 35;

                while (time <= 60)
                {
                    if (examination.DiaSis)
                    {
                        sis -= _rnd.Next(0, 3);
                        dia -= _rnd.Next(0, 2);
                        await Clients.Caller.SendAsync("newTonom", time, dia, sis);
                    }
                    if (examination.Tempareture)
                    {
                        temperature += _rnd.Next(0, 20) / 100.0;
                        await Clients.Caller.SendAsync("newTermom", time, temperature);
                    }
                    if (examination.HeartRate)
                    {
                        heartRate = _rnd.Next(90, 160);
                        averageHeartRate += heartRate;
                        await Clients.Caller.SendAsync("newHearRate", time, heartRate);
                    }
                    if (examination.SpO2)
                    {
                        sep = _rnd.Next(90, 99);
                        avarageSep += sep;
                        await Clients.Caller.SendAsync("newOximetr", time, sep);
                    }
                    time += 1;
                    Thread.Sleep(500);
                }

                await _mediator.Send(new FinishExaminationCommand { ExaminationId = id });
                await _mediator.Send(new CreateResultsCommand
                {
                    AverageDia = dia,
                    AverageSis = sis,
                    AverageTemperature = temperature,
                    AverageOxigen = avarageSep/60,
                    AverageHeartRate =averageHeartRate/60,
                    ExaminationId = examination.Id,
                    PatientId = examination.Patient.Id,
                    TypePhysicalActive = examination.TypePhysicalActive
                });
            }
        }

        public async Task TestTonomert()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await Clients.Caller.SendAsync("statusTonometr", status, count);
        }

        public async Task TestTermometr()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await Clients.Caller.SendAsync("statusTermometr", status, count);
        }

        public async Task TestHeartrate()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await Clients.Caller.SendAsync("statusHeartrate", status, count);
        }

        public async Task TestOximetr()
        {
            int count = _rnd.Next(0, 3);
            string status = string.Empty;
            if (count == 0)
            {
                status = "исправен";
            }
            if (count == 1)
            {
                status = "неисправен";
            }
            if (count == 2)
            {
                status = "занят";
            }
            await Clients.Caller.SendAsync("statusOximetr", status, count);
        }

        private void CountIndicators(Examination model)
        {
            var temp = model.Indicators;
            if (temp - 8 >= 0)
            {
                temp -= 8;
                model.SpO2 = true;
            }
            if (temp - 4 >= 0)
            {
                temp -= 4;
                model.HeartRate = true;
            }
            if (temp - 2 >= 0)
            {
                temp -= 2;
                model.Tempareture = true;
            }
            if (temp - 1 >= 0)
            {
                temp -= 1;
                model.DiaSis = true;
            }
        }
    }
}
