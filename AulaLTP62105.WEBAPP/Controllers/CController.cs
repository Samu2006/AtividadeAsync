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
            var resposta2 = Mensagem2(tempos.Tempo1, tempos.Tempo2);
            watch.Stop();
            
            watch2.Start();
            var tarefa1 = await Task.WhenAll(resposta1, resposta2);
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

        public async Task<string> Mensagem2(int tempo1, int tempo2)
        {
            var resposta3 = Mensagem(tempo1);
            var resposta4 = Mensagem(tempo2);

            var tarefa2 = await Task.WhenAny(resposta3, resposta4);
            return $"Mensagem escrita em {DateTime.Now}";
        }

    }
}