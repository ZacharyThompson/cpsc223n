default:
	mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:RicochetUI.dll ricochetui.cs
	mcs -r:System -r:System.Windows.Forms -r:RicochetUI.dll -out:Ricochet.exe ricochet.cs
run: default
	mono Ricochet.exe
clean:
	rm -f *.dll *.exe *.json
