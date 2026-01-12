namespace _2;

struct ComplexNum
{
    private double real;
    private double imaginary;

    public double Real
    {
        get
        {
            return real;
        }
        set
        {
            real = value;
        }
    }

    public double Imaginary
    {
        get
        {
            return imaginary;
        }
        set
        {
            imaginary = value;
        }
    }
           

    public ComplexNum(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    public ComplexNum()
    {
        this.real = 0;
        this.imaginary = 0;
    }

    public static ComplexNum operator +(ComplexNum firstNum, ComplexNum secondNum)
    {
        return new ComplexNum(firstNum.real + secondNum.real, firstNum.imaginary + secondNum.imaginary);
    }

    public static ComplexNum operator -(ComplexNum firstNum, ComplexNum secondNum)
    {
        return new ComplexNum(firstNum.real - secondNum.real, firstNum.imaginary - secondNum.imaginary);
    }

    public static ComplexNum operator *(ComplexNum firstNum, ComplexNum secondNum)
    {
        return new ComplexNum(firstNum.real * secondNum.real - firstNum.imaginary * secondNum.imaginary, firstNum.real * secondNum.imaginary + firstNum.imaginary * secondNum.real);
    }

    public static ComplexNum operator /(ComplexNum firstNum, ComplexNum secondNum)
    {
        double denominator = secondNum.real * secondNum.real + secondNum.imaginary * secondNum.imaginary;
        secondNum.imaginary = -secondNum.imaginary;
        ComplexNum temp = firstNum * secondNum;
        return new ComplexNum(temp.real / denominator, temp.imaginary / denominator);
    }

    public double Module()
    {
        return Math.Sqrt(real * real + imaginary * imaginary);
    }

    public double Argument()
    {
        if (real == 0) return Math.Sign(imaginary) * Math.PI / 2;
        else if (real > 0) return Math.Atan(imaginary / real);
        else return (imaginary >= 0) ? Math.Atan(imaginary / real) + Math.PI : Math.Atan(imaginary / real) - Math.PI; 
    }


    public override string ToString()
    {
        return (imaginary < 0) ? $"{real:G3} + ({imaginary:G3})i" : $"{real:G3} + {imaginary:G3}i";
    }

    

}