default:
	mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:MouseTagUI.dll mousetagui.cs
	mcs -r:System -r:System.Windows.Forms -r:MouseTagUI.dll -out:MouseTag.exe mousetagdriver.cs
run: default
	mono MouseTag.exe
clean:
	rm -f *.dll *.exe *.json

