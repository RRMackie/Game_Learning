Hello, Traveller!
Seems there is no way forward ... perhaps, I can help if you do me a favour first?
I have heard that there may be knowledge in the caves below. 
Get it for me and then we can proceed.

-> main

=== main ===
Have you searched? What did you find?
    + [Option1]
        -> Correct("Option 1")
    + [Option2]
        -> Incorrect("Option 2")
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