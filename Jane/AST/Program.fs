﻿namespace AST

open System
open System.Collections.Generic 

type Program(programMemberList : ProgramMember list, pos : Position) =
    inherit Node(pos)

    let mutable returnString = ""

    let members    = new Dictionary<string, ProgramMember>()
    let classes    = new Dictionary<string, Class>()
    let interfaces = new Dictionary<string, Interface>()

    let mutable mainClass     : Class option       = None
    let mutable mainMethod    : ClassMethod option = None
    let mutable errors        : Error list         = []
    let mutable nameMainClass : string             = ""
    

    member x.MemberList     = programMemberList
    member x.Members        = members
    member x.Classes        = classes
    member x.Interfaces     = interfaces
    
    member x.NameMainClass with get()    = nameMainClass
                            and set(str) = nameMainClass <- str
    
    member x.MainClass    with get()         = mainClass
                           and set(newClass) = mainClass <- newClass

    member x.MainMethod   with get()          = mainMethod
                           and set(newMethod) = mainMethod <- newMethod

    member x.Errors       with get()          = errors
                           and set(newErrors) = errors <- newErrors

    member x.ReturnString with get()    = returnString
                           and set(str) = returnString <- returnString + str

    member x.AddErrors newErrors = errors <- newErrors @ errors
    member x.AddError  newError  = errors <- newError :: errors

    override x.ToString() = programMemberList
                            |> List.map string
                            |> String.concat "\n\n" 
