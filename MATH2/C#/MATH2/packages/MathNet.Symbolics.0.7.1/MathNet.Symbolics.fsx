#nowarn "211"
#I "packages/MathNet.Symbolics/lib/net40"
#I "packages/MathNet.Symbolics.0.7.1/lib/net40"
#I "../packages/MathNet.Symbolics/lib/net40"
#I "../packages/MathNet.Symbolics.0.7.1/lib/net40"
#I "../../packages/MathNet.Symbolics/lib/net40"
#I "../../packages/MathNet.Symbolics.0.7.1/lib/net40"
#I "../../../packages/MathNet.Symbolics/lib/net40"
#I "../../../packages/MathNet.Symbolics.0.7.1/lib/net40"
#r "MathNet.Numerics.dll"
#r "MathNet.Numerics.FSharp.dll"
#r "MathNet.Symbolics.dll"

open MathNet.Symbolics

fsi.AddPrinter Infix.print