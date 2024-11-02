using Microsoft.ML;
using System;

namespace ML_Uninassau
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new MLContext();

            // Carregamento de dados
            var dadosDeTreinamento = context.Data.LoadFromTextFile<LivroData>("C:\\Users\\Ideapad 3i\\Documents\\Udemy\\Projetos C# O\\Machine_learning\\ML_Uninassau\\emprestimo.csv",
                hasHeader: true, separatorChar: ',');

            var testTrainoSplit = context.Data.TrainTestSplit(dadosDeTreinamento, testFraction: 0.2);

            // Construção do modelo
            var pipeline = context.Transforms.Categorical.OneHotEncoding(new[]
            {
                new InputOutputColumnPair("Nome"),
                new InputOutputColumnPair("Status")
            })
            .Append(context.Transforms.Concatenate("Features", "Nome", "Status")) // Concatena as colunas que foram transformadas
            .Append(context.Regression.Trainers.Sdca(labelColumnName: "Idade", maximumNumberOfIterations: 100));

            var model = pipeline.Fit(testTrainoSplit.TrainSet);

            // Avaliação
            var avaliacoes = model.Transform(testTrainoSplit.TestSet);
            var metricas = context.Regression.Evaluate(avaliacoes, labelColumnName: "Idade", scoreColumnName: "Score");

            Console.WriteLine($"R^2 - {metricas.RSquared}");

            // Previsão
            var novoDado = new LivroData() { Nome = "João", Status = "Ativo" }; // Exemplo de novos dados

            var previsaoFunc = context.Model.CreatePredictionEngine<LivroData, LivroPrevisao>(model);
            var previsao = previsaoFunc.Predict(novoDado);
            Console.WriteLine($"Previsão da Idade - {previsao.PrevisaoIdade}");
            Console.ReadLine();
        }
    }
}