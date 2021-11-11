#!/bin/sh

# Zachary Thompson
# CPSC 223N
# Midtern #1
# Oct 4, 2021

rm *.json *.dll *.exe -f
mcs -target:library -r:System.Drawing -r:System.Windows.Forms -out:Rolling_ball.dll Rolling_ball.cs
mcs -r:System -r:System.Windows.Forms -r:Rolling_ball.dll -out:TravelingBall.exe Animation-driver.cs

mono TravelingBall.exe
