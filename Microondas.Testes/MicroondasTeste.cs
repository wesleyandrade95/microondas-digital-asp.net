using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using avaliacao_wesleyandrade_microondas.Models;

namespace Microondas.Testes
{
    [TestClass]
    public class MicroondasTeste
    {
        [TestMethod]
        public void valida_pot_numerica()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();
            Programa pro = new Programa();
            pro.nome = "Frango";
            pro.tempo = 90;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
            pro.potencia = 7;
            pro.caractere = 'F';
            pro.original = true;
            programas.Add(pro);

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "pot aa";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = new List<String>();
            resultado_esperado.Add("A potência deve ser do tipo numérica");
            resultado_esperado.Add("Informe o tempo");

            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }

        [TestMethod]
        public void valida_entrada_program()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();

            Programa pro = new Programa();
            pro = new Programa();
            pro.nome = "Carne";
            pro.tempo = 100;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Carnes Vermelhas";
            pro.potencia = 10;
            pro.caractere = 'C';
            pro.original = true;
            programas.Add(pro);

            

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "program Carne";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = null;
            

            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }

        [TestMethod]
        public void valida_entrada_varios_comandos()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();
            Programa pro = new Programa();
            pro.nome = "Frango";
            pro.tempo = 90;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
            pro.potencia = 7;
            pro.caractere = 'F';
            pro.original = true;
            programas.Add(pro);

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "time 02:00,pot 10";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = null;


            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }

        [TestMethod]
        public void valida_entrada_quick()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();
            Programa pro = new Programa();
            pro.nome = "Frango";
            pro.tempo = 90;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
            pro.potencia = 7;
            pro.caractere = 'F';
            pro.original = true;
            programas.Add(pro);

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "quick";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = null;


            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }

        [TestMethod]
        public void valida_entrada_pot()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();
            Programa pro = new Programa();
            pro.nome = "Frango";
            pro.tempo = 90;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
            pro.potencia = 7;
            pro.caractere = 'F';
            pro.original = true;
            programas.Add(pro);

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "pot 10, time 00:37";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = null;


            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }

        [TestMethod]
        public void valida_entrada_pot_sem_time()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();
            Programa pro = new Programa();
            pro.nome = "Frango";
            pro.tempo = 90;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
            pro.potencia = 7;
            pro.caractere = 'F';
            pro.original = true;
            programas.Add(pro);

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "pot 10";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = new List<String>();
            resultado_esperado.Add("Informe o tempo");


            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }

        [TestMethod]
        public void valida_entrada_comando_desconhecido()
        {
            //Arrange
            avaliacao_wesleyandrade_microondas.Models.Microondas M;
            List<Programa> programas = new List<Programa>();
            Programa pro = new Programa();
            pro.nome = "Frango";
            pro.tempo = 90;
            pro.instrucoes = "Programa utilizado para preparo e descongelamento de Frangos";
            pro.potencia = 7;
            pro.caractere = 'F';
            pro.original = true;
            programas.Add(pro);

            M = new avaliacao_wesleyandrade_microondas.Models.Microondas(0, 0, 0, false, "", programas, -1);
            String entrada = "kkkkkk";
            M.comandos_entrada = entrada;

            List<String> resultado_esperado = new List<String>();
            resultado_esperado.Add("Comando não identificado");
            resultado_esperado.Add("Informe o tempo");
            

            //Act
            List<String> resultado_obtido = M.valida_entrada();

            //Assert
            Assert.AreEqual(resultado_esperado, resultado_obtido);

        }









    }
}
