using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AulaLTP62105.WEBAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace AulaLTP62105.WEBAPP.Controllers
{
    public class BController : Controller
    {
        public async Task<IActionResult> Marcador()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Marcador(TarefaB tempos)
        {

            CancellationTokenSource cancellationSourcer = new CancellationTokenSource();
            var token = cancellationSourcer.Token;

            var resposta1 = Mensagem(tempos.Tempo1, token);
            var resposta2 = Mensagem(tempos.Tempo2, token);


            var tarefa1 = await Task.WhenAny(resposta1, resposta2);
            cancellationSourcer.Cancel();
            var resposta3 = Mensagem(tempos.Tempo1);
            var resposta4 = Mensagem(tempos.Tempo2);
            var listTask = new List<Task> { resposta3, resposta4 };
            while (listTask.Count > 0)
            {
                var task = await Task.WhenAny(resposta3, resposta4);
                listTask.Remove(task);
            }

            string msg = $"Inicio {DateTime.Now}";

            msg += $" Termino {DateTime.Now}";

            var completo = $"Tarefa1:{resposta1.Result} ";
            completo += $" Tarefa2:{resposta2.Result} ";
            completo += $" Tarefa3:{resposta3.Result}";
            completo += $" Tarefa4:{resposta4.Result}";
            completo += $"Final: {msg} ";

            return View("Resultado", completo);
        }

        public IActionResult Resultado(string horarios)
        {
            return View(horarios);
        }


        public async Task<string> Mensagem(int tempo)
        {
            string msg = $"Inicio {DateTime.Now}";
            await Task.Delay(tempo);
            var resultado = msg + $" Mensagem escrita em {DateTime.Now}";
            return resultado;
        }
        public async Task<string> Mensagem(int tempo, CancellationToken cancellationTolken)
        {
            string msg = $"Inicio {DateTime.Now}";
            await Task.Delay(tempo);
            if (cancellationTolken.IsCancellationRequested)
                return "Tarefa Cancelada";
            var resultado = msg + $" Mensagem escrita em {DateTime.Now}";
            return resultado;

        }

    }
}