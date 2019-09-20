using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using avaliacao_wesleyandrade_microondas.Models;

namespace avaliacao_wesleyandrade_microondas.Controllers
{
    public class HomeController : Controller
    {

        [Filters.VerificaSessao]
        public ActionResult ExcluirPrograma(int id)
        {
            Microondas M;
            M = (Microondas)Session["microondas"];

            if(id < M.programas.Count && !(M.programas.ElementAt(id).original)) // é possivel fazer a exclusão
            {
                M.programas.RemoveAt(id);
                Session["microondas"] = M;

                
            }
            else // não foi possível fazer a exclusão
            {
                String erro="";
                if (id >= M.programas.Count)
                    erro = "Programa não localizado";
                else
                    if (M.programas.ElementAt(id).original)
                        erro = "Programas Originais do microondas não podem ser excluídos";

                ViewBag.erro_exclusao = erro;
            }

            return View("ListarProgramas", M.programas);
        }

        [HttpPost]
        public ActionResult BuscarProgramas(FormCollection form)
        {
            Microondas M;
            M = (Microondas)Session["microondas"];

            if (form!=null && form["busca"].ToString().Length>0) // tem algo digitado na busca
            {
                // faz a busca pelo nome
                String palavra_chave = form["busca"].ToString();
                List<Programa> resultado = new List<Programa>();

                for(int k=0;k<M.programas.Count;k++) // percorre os programas existentes
                {
                    if (M.programas.ElementAt(k).nome.ToLower().Contains(palavra_chave.ToLower())) // achou elemento que satisfaz a busca
                        resultado.Add(M.programas.ElementAt(k));
                }

                return View("ListarProgramas", resultado);
            }
            else // busca vazia, retorna todos
            {
                return View("ListarProgramas",M.programas);
            }
        }

        [Filters.VerificaSessao]
        public ActionResult ListarProgramas()
        {
            Microondas M;
            M = (Microondas)Session["microondas"];

            return View(M.programas);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if(form!=null)
            {
                String entrada = form["txtComandos"];
                Microondas M;
                M = (Microondas)Session["microondas"];

                M.comandos_entrada = entrada;
                List <String> Erros = M.valida_entrada();

                if(M.programa_selecionado>-1) // aplicar as configurações do programa selecionado
                {
                    M.segundos = M.programas.ElementAt(M.programa_selecionado).tempo;
                    M.minutos = 0;
                    M.potencia = M.programas.ElementAt(M.programa_selecionado).potencia;
                }
                else
                if (M.quick) // atribuo os valores aqui novamente, pq podem ter sido alterados porum comando time ou pot
                {
                    M.segundos = 30;
                    M.minutos = 0;
                    M.potencia = 8;
                }

                

                ViewBag.erros = Erros;

                ViewBag.entrada = entrada;

                Session["microondas"] = M;

                if(Erros==null) 
                    Session["estado"] = "aquecendo";
                else
                    Session["estado"] = "aguardando comando";
            }

            return View(); 
        }

        public ActionResult Index()
        {
            Microondas M;
            if (Session["microondas"] == null) // primeira inicialização do microondas, instanciar + atribuir programas pré estabelecidos
            {
                List<Programa> programas = new List<Programa>();

                Programa pro = new Programa();
                pro.nome = "Frango";
                pro.tempo = 90;
                pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
                pro.potencia = 7;
                pro.caractere = 'F';
                pro.original = true;
                programas.Add(pro);

                pro = new Programa();
                pro.nome = "Peixe";
                pro.tempo = 45;
                pro.instrucoes = "Programa utilizado para preparo e descongelamento de Peixes";
                pro.potencia = 6;
                pro.caractere = 'P';
                pro.original = true;
                programas.Add(pro);

                pro = new Programa();
                pro.nome = "Carne";
                pro.tempo = 100;
                pro.instrucoes = "Programa utilizado para preparo e descongelamento de Carnes Vermelhas";
                pro.potencia = 10;
                pro.caractere = 'C';
                pro.original = true;
                programas.Add(pro);

                pro = new Programa();
                pro.nome = "Descongelar";
                pro.tempo = 120;
                pro.instrucoes = "Programa utilizado para descongelamento de alimentos em geral";
                pro.potencia = 8;
                pro.caractere = 'D';
                pro.original = true;
                programas.Add(pro);

                pro = new Programa();
                pro.nome = "Cozinhar";
                pro.tempo = 110;
                pro.instrucoes = "Programa utilizado para cozimento de alimentos";
                pro.potencia = 9;
                pro.caractere = '*';
                pro.original = true;
                programas.Add(pro);

                M = new Microondas(0, 0, 0, false, "", programas, -1);
                Session["microondas"] = M;
            }
            return View();
        }

        [HttpGet]
        [Filters.VerificaSessao]
        public ActionResult NovoPrograma()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NovoPrograma(FormCollection form)
        {
            List<String> erros = new List<string>();

            if (form != null) // validando se o formulário foi preenchido corretamente
            {
                if(!(form["nome"]!=null && form["nome"].Length>0)) // validando nome
                    erros.Add("O nome do programa deve ser informado");

                if (!(form["instrucoes"] != null && form["instrucoes"].Length > 0)) // validando instrucoes
                    erros.Add("As instruções do programa deve ser informado");

                if(form["tempo"]!=null && form["tempo"].Length>0) // validando tempo
                {
                    int tempo = Convert.ToInt32(form["tempo"]);

                    if (!(tempo>0 && tempo <=120))
                        erros.Add("O tempo do programa deve estar entre 1 e 120 segundos");
                }
                else
                    erros.Add("O tempo do programa deve ser informado");


                if (form["potencia"] != null && form["potencia"].Length > 0) // validando potência
                {
                    int potencia = Convert.ToInt32(form["potencia"]);

                    if (!(potencia > 0 && potencia <= 10))
                        erros.Add("A potência do programa deve estar entre 1 e 10");
                }
                else
                    erros.Add("A potência do programa deve ser informado");

                if (!(form["caractere"] != null && form["caractere"].Length > 0)) // validando caractere de aquecimento
                    erros.Add("O caractere de aquecimento deve ser informado");

            }
            else
                erros.Add("Formulário foi submetido nulo");

            if(erros.Count>0) // posusi erros no formulário
            {
                ViewBag.erros_new_program = erros; // transportando erros para view, para exibição
                return View();
            }
            else // formulário preenchido corretamente
            {
                Programa p = new Programa();
                p.nome = form["nome"].ToString();
                p.instrucoes = form["instrucoes"].ToString();
                p.tempo = Convert.ToInt32(form["tempo"].ToString());
                p.potencia = Convert.ToInt32(form["potencia"].ToString());
                p.caractere = form["caractere"].ToString()[0];
                p.original = false;

                Microondas M = (Microondas)Session["microondas"];
                M.programas.Add(p);
                Session["microondas"] = M;
                return View("Index");
            }
            
        }

    }
}