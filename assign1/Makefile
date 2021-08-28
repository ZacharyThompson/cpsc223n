default:
	mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:Calculator.dll calculator.cs
	mcs -r:System -r:System.Windows.Forms -r:Calculator.dll -out:Circle.exe circle.cs
run: default
	mono Circle.exe
clean:
	rm -f *.dll *.exe
