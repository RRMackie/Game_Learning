Ah, so we meet again!
Seems like you are in the same predicament as before...

Perhaps there is more knowledge to be found somewhere nearby?
-> main

=== main ===
Oh you again, what did you find this time?
    + [Option1]
        -> Incorrect("Option 1")
    + [Option2]
        -> Correct("Option 2")
    + [Option3]
        -> Incorrect("Option 3")
    + [Goodbye]
        -> Leave("Goodbye")
        
=== Incorrect(Option) ===

No, that is not right...Oh and the monsters seemed to have followed you. Deal with them!
....or that's what I would say if it was programmed in. Try again!
-> END

=== Correct(Option) ===

Perfect, this will help in so many ways...
Oh yes, you wanted to move on? 
There, I have used my magic to make you a way forward! I do hope we meet again...
-> END

=== Leave(Option) ===

As you wish...
-> END