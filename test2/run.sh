#!/bin/bash

# Zachary Thompson
# Midterm #2
# November 8, 2021
# CPSC 223N-1

rm *.dll *.exe *.json -f

mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:RicochetUI.dll ricochetui.cs
mcs -r:System -r:System.Windows.Forms -r:RicochetUI.dll -out:Ricochet.exe ricochet.cs

mono Ricochet.exe
