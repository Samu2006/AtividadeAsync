using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AulaLTP62105.WEBAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace AulaLTP62105.WEBAPP.Controllers
{
    public class AController : Controller
    {
        public async Task<IActionResult> Marcador()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Marcador(TarefaA tempos)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            watch.Start();
            var resposta1 = Mensagem(tempos.Tempo1);
            var resposta2 = Mensagem(tempos.Tempo2);
            var resposta3 = Mensagem3();

            var tarefa1 = Task.WhenAll(resposta1, resposta2);
            watch.Stop();

            watch2.Start();
            var resposta4 = Mensagem(tempos.Tempo1);

            var tarefa2 = Task.WhenAll(resposta3, resposta4);

            var resltado = await Task.WhenAll(tarefa2, tarefa1);
            watch2.Stop();
            var completo = $"Tarefas terminaram {watch.Elapsed.TotalMilliseconds} e {watch2.Elapsed.TotalMilliseconds}";

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

        public async Task<string> Mensagem3()
        {
            await Task.Delay(2000);

            var resposta5 = Mensagem(1000);
            var resposta6 = Mensagem(2000);
            var tarefa = Task.WhenAll(resposta5, resposta6);

            return "Tarefa3 Concluida";
        }
    }
}