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
            var resposta3 = Mensagem2(tempos.Tempo3);

            var tarefa1 = Task.WhenAll(resposta1, resposta2);
            watch.Stop();

            watch2.Start();
            var resposta4 = Mensagem3(tempos.Tempo1, tempos.Tempo2);

            var tarefa2 = Task.WhenAll(resposta3, resposta4);

            string msg = $"Inicio {DateTime.Now}";
            var resltado = await Task.WhenAll(tarefa2, tarefa1);
            msg += $" Termino {DateTime.Now}";

            watch2.Stop();
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

        public async Task<string> Mensagem2(int tempo)
        {
            string msg = $"Inicio {DateTime.Now}";
            await Task.Delay(tempo);

            var resposta5 = Mensagem(1000);
            var resposta6 = Mensagem(2000);
            var tarefa = Task.WhenAll(resposta5, resposta6);
            var resultado = msg + $" Mensagem escrita em {DateTime.Now} ";
            resultado += $"Tarefa5:{resposta5.Result}";
            resultado += $"Tarefa6:{resposta6.Result}";
            return resultado;

        }

        public async Task<string> Mensagem3(int tempo1, int tempo2)
        {

            var msg1 = Mensagem(tempo1);
            var msg2 = Mensagem(tempo2);

            var tarefa3 = await Task.WhenAll(msg1, msg2);
            var msg = Mensagem(tempo1 + tempo2);
            return msg.Result;
        }
    }

}