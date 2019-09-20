using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avaliacao_wesleyandrade_microondas.Models
{
    public class Microondas
    {
        public int minutos { get; set; }
        public int segundos { get; set; }
        public int potencia { get; set; }

        public Boolean quick { get; set; }

        public String comandos_entrada { get; set; }

        public List<Programa> programas { get; set; }

        public int programa_selecionado; // salva index do programa selecionado
        public int informou_potencia { get; set; } // atributo auxiliar, apenas para verificar se a potência foi informada, caso controário é utilizado a potência default.

        public Microondas(int minutos, int segundos, int potencia, Boolean quick, String comandos_entrada, List<Programa> programas,int programa_selecionado)
        {
            this.minutos = minutos;
            this.segundos = segundos;
            this.potencia = potencia;
            this.quick = quick;
            this.comandos_entrada = comandos_entrada;
            this.programas = programas;
            this.programa_selecionado = programa_selecionado;
        }

        public String valida_comando(String comando)
        {
            String erro = "";

            if (comando.ToLower().Contains("pot")) // valida comando referente a potência
            {
                if (comando.Length > 3 && comando.Length <= 5)
                {
                    if(comando.Substring(0,3).Equals("pot"))
                    {
                        int pot = -1;
                        try
                        {
                            int tamanho = comando.Length;
                            pot = Convert.ToInt32(comando.Substring(3));
                            if (!(pot > 0 && pot <= 10))
                            {
                                erro = "A potência deve estar entre 1 e 10";
                            }
                            else
                            {
                                this.potencia = pot;
                                informou_potencia = 1;
                            }
                                
                        }
                        catch(Exception e)
                        {
                            erro = "A potência deve ser do tipo numérica";
                        }
                    }
                    else
                        erro = "A sintaxe do comando pot está errada";
                    
                }
                else
                    erro = "Comando pot possui ausência ou excesso de parâmetros";
            }
            else
            if (comando.ToLower().Contains("time")) // valida comando referente a tempo
            {
                if (comando.Length == 9)
                {
                    if (comando.Substring(0, 4).Equals("time") && comando[6]==':') 
                    {
                        int min, seg;
                        
                        try
                        {
                            min = Convert.ToInt32(comando.Substring(4, 2));
                            
                            seg = Convert.ToInt32(comando.Substring(7));

                            if(min >= 0 && seg >= 0)
                            {
                                if (!(((min*60)+seg)<=120 && ((min * 60) + seg)>0))
                                {
                                    erro = "O tempo não pode exceder o equivalente a 2 minutos nem ser inferior a 1 segundo";
                                }
                                else
                                {
                                    this.minutos = min;
                                    this.segundos = seg;
                                }
                            }
                            else
                                erro = "O tempo não pode conter parâmetros negativos";

                        }
                        catch(Exception e)
                        {
                            erro = "O tempo deve ser do tipo numérica";
                        }
                    }
                    else
                        erro = "A sintaxe do comando time está errada";
                }
                else
                    erro = "Comando time possui ausência ou excesso de parâmetros";
                
            }
            else
                if(comando.ToLower().Contains("program")) // valida comando referente a utilização de programas 
                {
                this.programa_selecionado = -1;
                    if (comando.Length > 7)
                    {
                    String nome_programa = comando.Substring(7).ToLower();
                    // busca programa entre os existentes no microondas

                    if(this.programas!=null && this.programas.Count>0) // se possui programas para buscar
                    {
                        for(int j=0; j<programas.Count && programa_selecionado==-1; j++)
                        {
                            if(programas.ElementAt(j).nome.ToLower().Trim().Replace(" ","").Equals(nome_programa)) // achou programa
                            {
                                this.programa_selecionado = j;
                            }
                        }

                        if(this.programa_selecionado == -1) // não encontrou programa
                            erro = "Programa não localizado";
                    }
                    else
                    {
                        erro = "O microondas não possui programas cadastrados, realize o cadastro antes de prosseguir";
                    }

                    }
                    else
                    {
                        erro = "Comando program possui ausência de parâmetros";
                    }
                }
                else
                    if (comando.ToLower().Equals("quick")) // valida comando referente a início rápido
                    {
                        this.quick = true;
                        this.potencia = 8;
                        this.minutos = 0;
                        this.segundos = 30;
                    }
                    else
                        erro = "Comando não identificado";

                    return erro;

        }
        public List<String> valida_entrada()
        {
            informou_potencia = 0;
            this.quick = false;

            List<String> erros = null;

            if (comandos_entrada != null && comandos_entrada.Length > 0) // se existe uma string para ser analizada
            {
                this.comandos_entrada = comandos_entrada.Trim(); // retirando espaços da string (apenas para quesitos de otimização e facilidade no tratamento dos comandos.
                this.comandos_entrada = this.comandos_entrada.Replace(" ", "");

                int index_inicial = 0;
                int index_auxiliar = 0;
                String comando = "";

                while(index_inicial< comandos_entrada.Length) // enquanto não analizou a string inteira
                {
                    index_auxiliar = comandos_entrada.IndexOf(",", index_inicial); // pega o index onde aparece ","

                    if(index_auxiliar>0)
                    {
                        comando = comandos_entrada.Substring(index_inicial, index_auxiliar- index_inicial);
                        index_inicial = index_auxiliar + 1; // proxima analise ele começa uma posição á frente da quale ele encontrou o "," na analise anterior
                    }
                    else 
                    {
                        if(index_auxiliar == -1) // não encotrou "," , pega o restante da string
                        {
                            comando = comandos_entrada.Substring(index_inicial);
                            index_inicial = comandos_entrada.Length; // força o fim da analise (while)
                        }
                        else
                            if (index_auxiliar == index_inicial) // encontrou no começo da string
                            {
                                comando = comandos_entrada[index_auxiliar].ToString();
                                index_inicial++;
                            }

                    }

                    // chama método que verifica se o comando é valido e retorna msg de erro.
                    String erro = this.valida_comando(comando);

                    if(erro.Length>0) // encontrou erro no comando
                    {
                        if (erros == null) // se o list ainda for nulo, instancia ele (ocorre na primeira vez que encontra erros)
                            erros = new List<string>();

                        erros.Add(erro);
                    }
                    
                }
                    

            }

            if (informou_potencia == 0 && this.quick == false) // se não foi informado potencia e não está no modo inicio rápido, atribui potência padrão
                this.potencia = 10;

            if (this.minutos==0 && this.segundos==0) // Não foi informado tempo 
            {
                if(programa_selecionado == -1)
                {
                    if (erros == null) // se o list ainda for nulo, instancia ele (ocorre na primeira vez que encontra erros)
                        erros = new List<string>();

                    erros.Add("Informe o tempo");
                }
                
            }

            return erros;
        }
    }

    
}