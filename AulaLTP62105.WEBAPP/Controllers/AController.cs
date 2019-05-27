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
            var resposta3 = Mensagem(tempos.Tempo3);

            await Task.WhenAll(resposta1, resposta2, resposta3);
            watch.Stop();

            watch2.Start();
            var resposta4 = Mensagem(tempos.Tempo1 + tempos.Tempo3);
            var resposta5 = Mensagem(tempos.Tempo2 + tempos.Tempo3);
            var resposta6 = Mensagem(tempos.Tempo1 + tempos.Tempo2);

            await Task.WhenAll(resposta4, resposta5, resposta6);
            watch2.Stop();
            var completo = $"Tarefas terminaram {watch.Elapsed.TotalMilliseconds} e {watch2.Elapsed.TotalMilliseconds}";
            //$"{resposta1.GetAwaiter().GetResult().ToString()}" +
            //$"{resposta2.GetAwaiter().GetResult().ToString()}" +
            //$"{resposta3.GetAwaiter().GetResult().ToString()}" + 
            //$"{resposta4.GetAwaiter().GetResult().ToString()}" + 
            //$"{resposta5.GetAwaiter().GetResult().ToString()}" + 
            //$"{resposta6.GetAwaiter().GetResult().ToString()}";
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
    }
}