default:
	mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:StopLightUI.dll StopLightUI.cs
	mcs -r:System -r:System.Windows.Forms -r:StopLightUI.dll -out:StopLight.exe StopLight.cs
run: default
	mono StopLight.exe
clean:
	rm -f *.dll *.exe *.json
