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
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            watch.Start();
            CancellationTokenSource cancellationSourcer = new CancellationTokenSource();
            var token = cancellationSourcer.Token;

            var resposta1 = Mensagem(tempos.Tempo1, token);
            var resposta2 = Mensagem(tempos.Tempo2, token);

            var resposta3 = Mensagem(tempos.Tempo2 + tempos.Tempo1, token);

            var tarefa1 = await Task.WhenAny(resposta1, resposta2);
            cancellationSourcer.Cancel();

            watch.Stop();
            watch2.Start();

            var tarefa2 = await Task.WhenAll(tarefa1,resposta3);


            watch2.Stop();
            var completo = $"Tarefas terminaram {watch.Elapsed} e {watch2.Elapsed}";

            return View("Resultado", completo);
        }

        public IActionResult Resultado(string horarios)
        {
            return View(horarios);
        }


        public async Task<string> Mensagem(int tempo)
        {
            await Task.Delay(tempo);
            return $"Mensagem escrita em {DateTime.Now}";
        }
        public async Task<string> Mensagem(int tempo, CancellationToken cancellationTolken)
        {

            if (cancellationTolken.IsCancellationRequested)
                return null;
            await Task.Delay(tempo);
            return $"Mensagem escrita em {DateTime.Now}";

        }
    }
}