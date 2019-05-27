using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AulaLTP62105.WEBAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace AulaLTP62105.WEBAPP.Controllers
{
    public class CController : Controller
    {
        public async Task<IActionResult> Marcador()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Marcador(TarefaC tempos)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            watch.Start();

            var resposta1 = Mensagem(tempos.Tempo1);
            var resposta2 = Mensagem(tempos.Tempo2);

            await Task.WhenAll(resposta1, resposta2);

            watch.Stop();

            watch2.Start();
            var resposta3 = Mensagem(tempos.Tempo1/2);
            var resposta4 = Mensagem(tempos.Tempo1/2);
            await Task.WhenAll(resposta1,resposta3, resposta4);
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
       
    }
}