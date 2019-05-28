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
           

            var resposta1 = Mensagem(tempos.Tempo1);
            var resposta2 = Mensagem2(tempos.Tempo1, tempos.Tempo2);

            string msg = $"Inicio {DateTime.Now.ToLongTimeString()}";
            var tarefa1 = await Task.WhenAll(resposta1, resposta2);
            var resultado = msg + $" Mensagem escrita em {DateTime.Now.ToLongTimeString()}";

            var completo = $"Tarefa1:{resposta1.Result}" +
                $"Tarefa2:{resposta2.Result}" +
                $"Resultado:{resultado}";

            return View("Resultado", completo);
        }

        public IActionResult Resultado(string horarios)
        {
            return View(horarios);
        }


        public async Task<string> Mensagem(int tempo)
        {
            string msg = $"Inicio {DateTime.Now.ToLongTimeString()}";
            await Task.Delay(tempo);
            var resultado = msg + $" Mensagem escrita em {DateTime.Now.ToLongTimeString()}";
            return resultado;
        }

        public async Task<string> Mensagem2(int tempo1, int tempo2)
        {
            var msg = await Mensagem(tempo2);
            var resposta3 = Mensagem3(tempo1, tempo1 + tempo2);

            var resultado = msg.ToString();
            resultado += resposta3.Result;
            return resultado;
        }
        public async Task<string> Mensagem3(int tempo1, int tempo2)
        {
            var resposta3 = Mensagem(tempo1 + tempo2);
            var resposta4 = Mensagem(tempo2);
            string msg = $"Inicio {DateTime.Now.ToLongTimeString()}";
            var tarefa2 = await Task.WhenAny(resposta3, resposta4);
            var resultado = msg + $" Mensagem escrita em {DateTime.Now.ToLongTimeString()}";
            return resultado;
        }

    }
}