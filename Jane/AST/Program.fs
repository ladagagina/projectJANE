﻿namespace AST

open System 

type Program(programMembers : ProgramMember list, nameMainClass : string, pos : Position) =
    inherit Node(pos)
    
    let classes    = List.fold (fun acc (m : ProgramMember) -> 
                                    try m :?> Class :: acc 
                                    with :? InvalidCastException -> acc
                               ) [] programMembers

    let interfaces = List.fold (fun acc (m : ProgramMember) -> 
                                    try m :?> Interface :: acc 
                                    with :? InvalidCastException -> acc
                               ) [] programMembers

    member x.ProgramMembers = programMembers
    member x.NameMainClass  = nameMainClass
    member x.Classes        = classes
    member x.Interfaces     = interfaces

    override x.ToString() = programMembers
                            |> List.map string
                            |> String.concat "\n\n" 

    //interpret the main method in the class that contains it
    //mainclass sholud be in head of ProgramMembers, mainMethod sholud be in head of Members.
    member x.Interpret() =
        let classWithMain = classes.Head
        let mainMethod = classWithMain.VoidMethods.Head
        mainMethod.Interpret()
