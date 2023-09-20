using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;



/*
Для того, чтобы найти решение необходимо
1.создать/загрузить начальную матрицу А и итоговый вектор х
2.построить приближенную матрицу М для начальной А
3.представить матрицу М через LU-разложение (она должна быть легко вычислима, легко обратима)
4.с помощью этого найти обратную матрицу М
5.получить искомый вектор при умножении обартной матрицы М на вектор х

большая часть времени ушла на разбор данного метода, не работала раньше с ним. как поняла неполное разложение отличается тем, что раскладывается не изначальная матрица, а приближенная
сначала создавала матрицы через обычное создание массивов, с чиклами for по столбикам и по строкам
потом нашла библиотеку, которая работает с матрицами,векторами, и решила применять его возможности
пока не знаю, как выполнить все условия при создании разряженной матрицы, работаю над этим
и какие услови должны быть у матрицы погрешностей

буду ждать обратной связи, чтобы понять правильно ли я поняла задачу и описала алгорит выше
*/
namespace ILU_Method
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("Введите размер квадратной матрицы в диапазоне от 100 до 10000: "); //ввести размер матрицы
      

            //Вводим и читаем размер
            int n = Convert.ToInt16(Console.ReadLine()); //считать размер матрицы

            // if (n >= 100 && n <= 10000) //проверка условий
            // {


            var M = Matrix<double>.Build;

            Console.WriteLine("Исходная матрица"); //исходная матрица
            var MatrixA = M.DenseDiagonal(n, n, 1.0); //создание матрицы с единичной диагональю
            Console.WriteLine(MatrixA);

            Console.WriteLine("Вектор Х"); // итоговый вектор
            Vector<double> VectorX = Vector<double>.Build.Random(n);
            Console.WriteLine(VectorX);

            Console.WriteLine("Матрица погрешностей"); // матрица погрешностей
            var MatrixError = M.Random(n, n);
            Console.WriteLine(MatrixError);


            Console.WriteLine("Приближенная матрица");// приближенная матрица M
            var m1 = MatrixA + MatrixError; 
            Console.WriteLine(m1);
           

            LU<double> lu = m1.LU();         // LU разложение
            var l = lu.L;
            var u = lu.U;
            Console.WriteLine("L:"+ l);
            Console.WriteLine("U:" + u);


            var l1 = l.Inverse();           //обратные матрицы LU 
            var u1 = u.Inverse();

            Console.WriteLine("Обратная матрица М");
            var m2 =u1.Multiply(l1);            //обратная матрицы М
            Console.WriteLine(m2);



            Console.WriteLine("Результат умножения приближенной матрицы");
            var y = m2.Multiply(VectorX);
            Console.WriteLine(y);

         



            //}



            // else Console.WriteLine("Число вне диапазона");

        }
}
}
