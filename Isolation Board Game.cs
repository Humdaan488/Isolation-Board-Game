using System;
using static System.Console;
using static System.Math;

namespace Bme121
{
    static class Program
    {
        // field variables
        static bool[ , ] board; // boolean variable holding the places on the board where the game will be played
        static int rows, columns; // number of rows and columns on game board.
        static int platformRowA, platformColA; // location of platform A (starting)
        static int platformRowB, platformColB; // location of platform B (starting)
        static int pawnRowA, pawnColA; // pawn A location (pawns used to play the game)
        static int pawnRowB, pawnColB; // pawn B location
        static string name, secondName; // names of the players
        static bool gameRunning = true; // bool variable to illustrate if players can keep playing
        static int rowChangeA, colChangeA; // the pawn changing position for A
        static int pRowRemoveA, pColRemoveA; // the tile removal requested by A
        static int rowChangeB, colChangeB; // the pawn changing position for B
        static int pRowRemoveB, pColRemoveB; // the tile removal requested by B

        // Basic Main method that just repeatedly loops the Game Board and Moves method
        static void Main( ) 
        {
            Initialization( );
            WriteLine ("Welcome to the fascinating game of Isolation! Have fun.");
            
            while (gameRunning == true)
            {
                DrawGameBoard( );
                MakeMoves ( );
            }
        }
                
// ------------------------------Initialization Method----------------------------------------------
        static void Initialization ( )
        {
            Console.Clear ( );
            WriteLine ( );
            WriteLine ("Welcome to the fascinating game of Isolation");
            
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
                
            int [ ] numbers = new int [26];
            
            for ( int i = 0; i < numbers.Length; i++ )
            {
                numbers [i] = i;
            }

            // Player A and B Name inputs
            Write ( "Please enter your name [default Player A]: " );
            name = ReadLine ();
            if ( name.Length == 0 ) name = "Player A" ; // default 
            WriteLine ( "Name: {0}", name);

            Write ( "Please enter your name [default Player B]: " );
            secondName = ReadLine ();
            if ( secondName.Length == 0 ) secondName = "Player B" ; // default 
            WriteLine ( "Name: {0}", secondName);
            
            bool isValid;      
            isValid = false;
            
            // Getting the number of rows and columns for the board + error checking
            while ( isValid == false )                 
            {
                Write ("Enter the number of board rows [4-26, default 6]: ");
                string response1 = ReadLine ();
                if ( response1.Length == 0 ) rows = 6;
                else rows = int.Parse (response1);
                if ( rows < 4 || rows > 26 )
                {
                    WriteLine ("The number of rows entered must be between 4 and 26");
                }
                else isValid = true;
            }
            
            // Same thing with columns so set it to false in case it was true after the action above
            isValid = false; 
                
            while ( isValid == false )                 
            {
                Write ("Enter the number of board columns [4-26, default 8]: ");
                string response1 = ReadLine ();
                if (response1.Length == 0 ) columns = 8;
                else columns = int.Parse (response1);
                if ( columns < 4 || columns > 26 )
                {
                    WriteLine ("The number of columns entered must be between 4 and 26");
                }
                else isValid = true;
            }
            
            // Setting up the board with what user requested
            board = new bool [ rows, columns ];
            // Going through array to set all the tiles to be true so its easier to set false later
            for ( int a = 0; a < rows; a ++ )
            {
                for ( int j = 0; j < columns ; j ++ )
                {
                    board [a,j] = true; // we do not know what going to be there we set it to true because we know there is gonna be a tile there
                }
            }
            
            // Start of Initialization of players initial platforms (starting position for pawn)
                        
            // starting position of Pawn A
            Write ("Enter a letter for Pawn A's Platform Row or Press Enter for Default :");
            string response = ReadLine ( );

            platformRowA = Array.IndexOf (letters,response); // makes the connection between letters and numbers from the 2 arrays
            
            // default values
            if (response.Length == 0)
            {
                platformRowA = pawnRowA = (int) Math.Ceiling ( (rows - 1) / 2.0 );
                WriteLine ("The default row is {0}", letters [ platformRowA ]);
            }
            
            //if the user enters more than one letter
            else if (response.Length != 1)
            {
                Write ("Please enter 1 letter for Pawn A's platform or press enter for default: ");
                response = ReadLine ( );
            }
            
            while ( platformRowA > rows - 1)
            {
                Write( "Enter a valid letter for Pawn A's platform row or press enter for default: " );
                response = ReadLine ( );
                platformRowA = Array.IndexOf ( letters, response );
            }
                        
            Write ( "Enter a letter for Pawn A's platform column or press enter for default: " );
            response = ReadLine( );
            platformColA = Array.IndexOf ( letters, response );
            
            if  (response.Length == 0 )
            {
                platformColA = 0;
                WriteLine ( "The default column is {0}", letters [ platformColA ]  );
            }

            // if user enters more than one letter
            else if ( response.Length != 1 )
            {
                Write ( "Please enter 1 letter for Pawn A's platform column or press enter for default: " );
                response = ReadLine( );
            }

            while ( platformColA > columns - 1)
            {
                Write( "Enter a valid letter for Pawn A's platform column or press enter for default: " );
                response = ReadLine ( );
                platformColA = Array.IndexOf ( letters, response );
            }


            // declaring starting position of Pawn B - PLAYER B TIME
            Write( "Enter a Letter for Pawn B's Platform Row or Press Enter: " );
            response = ReadLine( );
            platformRowB = Array.IndexOf(letters, response);
            
            // default values 
            if ( response.Length == 0 )
            {
                platformRowB = pawnRowB = ( int ) Math.Ceiling( ( rows - 1 ) / 2.0 + 1.0 );
                WriteLine("The default row is {0}", letters[ platformRowB ] );
            }

            // if user enters more than one letter
            else if ( response.Length != 1 )
            {
                Write ( "Please enter 1 letter for Pawn B's platform row or press enter for default: " );
                response = ReadLine( );
            }
            
            while ( platformRowB > rows - 1 || platformRowB == platformRowA )
            {
                Write( "Enter a valid letter for Pawn B's platform row or Press Enter: " );
                response = ReadLine( );
                platformRowB = Array.IndexOf( letters, response );
            }
            
            Write( "Enter a Letter for Pawn B's Platform Column or Press Enter: " );
            response = ReadLine( );
            platformColB = Array.IndexOf(letters, response);
            
            // default values
            if ( response.Length == 0 )
            {
                platformColB = board.GetLength( 1 ) - 1;
                WriteLine( "The default row is {0}", letters[ platformColB ] );
            }
            
            // if the user enters more than one letter
            else if ( response.Length != 1 )
            {
                Write ( "Please enter 1 letter for Pawn B's platform column or press enter for default: " );
                response = ReadLine( );
            }
            
            while ( platformColB > columns - 1 || platformColB == platformColA )
            {
                Write( "Please Enter a Valid Letter for Pawn B's Platform Row or Press Enter: " );
                response = ReadLine( );
                platformColB = Array.IndexOf( letters, response );
            }
                        
            // This gives a position to the platforms as well as the pawn
            pawnRowA = platformRowA;
            pawnColA = platformColA;
            pawnRowB = platformRowB;
            pawnColB = platformColB;
        }
        
        // DrawGameBoardMethod----------------------------------------------------------------------
        
        static void DrawGameBoard( )
        {
            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            //const string sp = " ";      // space
            //const string pa = "A";      // pawn A
            //const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // block
            const string fb = "\u2588"; // left half block
            //const string lh = "\u258c"; // left half block
            //const string rh = "\u2590"; // right half block
            
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
             
             
                
            // Draw the top board boundary.
            
            Write( "    " );
            for(int p = 0; p < board.GetLength(1); p++)
            {
                Write(" {0}  ", letters[ p ] );
            }
            WriteLine ( );

            
            Write( "   " );
            for( int c = 0; c < board.GetLength( 1 ); c ++ )
            {
                if( c == 0 ) Write( tl );
                Write( "{0}{0}{0}", h );
                if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", tr ); 
                else                                Write( "{0}", hb );
            }
            WriteLine( );
            
            
            
            
            // Draw the board rows.
            for( int r = 0; r < board.GetLength( 0 ); r ++ )
            {
                Write( " {0} ", letters[ r ] );
                
                // Draw the row contents.
                for( int c = 0; c < board.GetLength( 1 ); c ++ )
                {
                    if( c == 0 ) Write( v );
                    if( board[ r, c ] ) 
                    {
                        if ( r == pawnRowA && c == pawnColA) Write ( " A " + v);
                        else if ( r == pawnRowB && c == pawnColB ) Write ( " B " + v);
                        else if ( r == platformRowA && c == platformColA ) Write ( " {0} {1}", bb, v);
                        else if ( r == platformRowB && c == platformColB ) Write ( " {0} {1}", bb, v);
                        else Write ( " {0} {1}" , fb, v);
                    }
                    else   Write( "{0}{1}", "   ", v ); // tile removed
                       
                }
                WriteLine( );
                
                // Draw the boundary after the row.
                if( r != board.GetLength( 0 ) - 1 )
                { 
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( vr );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", vl ); 
                        else                                Write( "{0}", hv );
                    }
                    WriteLine( );
                }
                
                else
                {
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( bl );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", br ); 
                        else                                Write( "{0}", ha );
                    }
                    WriteLine( );
                }
            }
        }
        
        //-------- Making a Moves Method---------------------------------------------
    
        static void MakeMoves ( )
        {
            bool isPossible = false; // boolean flag assuming it's false until proven true used to check for both Player A and B
            
            string [ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z" };
            
            while(isPossible == false)
            {
                WriteLine();
                WriteLine("{0}, it's your turn", name); // displaying which players turn it is
                WriteLine();
                
                //prompt to get move from user
                Write( "Please enter a 4 letter move, move to [ab] remove [cd] : "); 
                string move = ReadLine( );

                while (move.Length != 4) // to ensure move is four letters
                {
                    Write("That is not a 4 letter move re-enter a 4 letter move: ");
                    move = ReadLine();
                }
                
                // Changing all letters to numbers and perfroming the correct action with each letter
                rowChangeA = Array.IndexOf(letters, move.Substring(0,1)); // This is the first part of the move
                colChangeA = Array.IndexOf(letters, move.Substring(1,1)); // this is the second part
                pRowRemoveA = Array.IndexOf(letters, move.Substring(2,1)); // this is the removal rows
                pColRemoveA = Array.IndexOf(letters, move.Substring(3,1)); // this is for the removal of column

                // A while loop to ensure the move that the player wants to make is within the board bounds and if not user re-enters
                while (rowChangeA > board.GetLength(0) - 1 || colChangeA > board.GetLength(1) -1)
                {
                    WriteLine ("Please enter a value to be within board: ");
                    move = ReadLine();

                    rowChangeA = Array.IndexOf(letters, move.Substring(0,1)); // This is the first part of the move
                    colChangeA = Array.IndexOf(letters, move.Substring(1,1)); // this is the second part
                    pRowRemoveA = Array.IndexOf(letters,move.Substring(2,1)); // this is the removal rows
                    pColRemoveA = Array.IndexOf(letters,move.Substring(3,1)); // this is for the removal of column
                }

            //If-else sttatements that are found below ensure move is logically correct

            //this checks that A does move and doesn't stay on the same tile
            if (rowChangeA == pawnRowA && colChangeA == pawnColA)
            {
                WriteLine("You are on this tile");
            }

            // Makes sure A doesn't move onto B
            else if(rowChangeA == pawnRowB && colChangeA == pawnColB)
            {
                WriteLine("Pawn B is found on this Tile");
            }

            //making sure that player does not move to a removed(false) tile
            else if (board [rowChangeA,colChangeA] == false)
            {
                WriteLine("You can't move to a removed tile");
            }

            //This is making sure that the player is not moving more than 1 in the column and row direction
            else if((pawnRowA - 1 > rowChangeA || rowChangeA > pawnRowA + 1) || (pawnColA - 1 > colChangeA || colChangeA > pawnColA + 1) )
            {
                WriteLine(" Sorry but you can only move to an adjacent tile in the row and column direction");
            }

            // checking removal of tiles
            else if ((pRowRemoveA == pawnRowA && pColRemoveA == pawnColA) ||
                    (pRowRemoveA == platformRowA && pColRemoveA == platformColA) || (pRowRemoveA == platformRowB && pColRemoveA == platformColB) ||
                    (pRowRemoveA == rowChangeA && pColRemoveA == colChangeA) || (pRowRemoveA == pawnRowB && pColRemoveA == pawnColB) ||
                    (pRowRemoveA > board.GetLength(0) - 1 || pColRemoveA > board.GetLength(1) - 1) ||
                    (board[pRowRemoveA,pColRemoveA] == false))
            {
                WriteLine("Error!This tile cannot be removed.");
            }

            // This else statement says that if all the conditions are met then update the position of pawn A, remove the tile specified and then exit the loop
            else
            {
                // making the correct move
                pawnRowA = rowChangeA;
                pawnColA = colChangeA;
                board[pRowRemoveA, pColRemoveA] = false; // tile is now removed
                isPossible = true;
            }
          }
            
            DrawGameBoard();


            isPossible = false;
            
            while(!isPossible)
            {
                WriteLine();
                WriteLine("{0}, it's your turn", secondName); // displaying which players turn it is
                WriteLine();

                Write( "Please enter a 4 letter move, move to [ab] remove [cd] : "); //prompt to get move from user
                string move = ReadLine( );

            while (move.Length != 4) // 
            {
                Write("That is not a 4 letter move re-enter a 4 letter move: ");
                move = ReadLine();
            }

            rowChangeB = Array.IndexOf (letters, move.Substring(0,1)); // This is the first part of the move
            colChangeB = Array.IndexOf (letters, move.Substring(1,1)); // this is the second part
            pRowRemoveB = Array.IndexOf (letters, move.Substring(2,1)); // this is the rows removal
            pColRemoveB = Array.IndexOf (letters, move.Substring(3,1)); // this is for the removal of column

            // A while loop to ensure the move that the player wants to make is within the board bounds
            while (rowChangeB > board.GetLength(0) - 1 || colChangeB > board.GetLength(1) -1)
            {
                WriteLine ("Please enter a value be within board: ");
                move = ReadLine();

                rowChangeA = Array.IndexOf(letters, move.Substring(0,1)); // This is the first part of the move
                colChangeA = Array.IndexOf(letters, move.Substring(1,1)); // this is the second part
                pRowRemoveA = Array.IndexOf(letters,move.Substring(2,1)); // this is the removal rows
                pColRemoveA = Array.IndexOf(letters,move.Substring(3,1)); // this is for the removal of column
            }

                //If-else sttatements that are found below ensure move is logically correct for B just like we did for A
                
                //this checks that A does move and doesn't stay on the same tile
            if (rowChangeB == pawnRowB && colChangeB == pawnColB)
            {
                WriteLine("You are on this tile");
            }

                
            else if(rowChangeB == pawnRowA && colChangeB == pawnColA)
            {
                WriteLine("Pawn A is found on this Tile");
            }

                //making sure that player does not move to a false tile
            else if (board[rowChangeA,colChangeA] == false)
            {
                WriteLine("You can't move to a removed tile");
            }

                //This is making sure that the player is not moving more than 1 in the column and row direction
            else if((pawnRowB - 1 > rowChangeB || rowChangeB > pawnRowB + 1) || (pawnColB - 1 > colChangeB || colChangeB > pawnColB + 1) )
            {
                WriteLine(" Sorry but you can't move more than 1 box in the row and column direction");
            }

               
            else if ((pRowRemoveB == pawnRowB && pColRemoveB == pawnColB) ||
                    (pRowRemoveB == platformRowB && pColRemoveB == platformColB) || (pRowRemoveB == platformRowA && pColRemoveB == platformColA) ||
                    (pRowRemoveB == rowChangeB && pColRemoveB == colChangeB) || (pRowRemoveB == pawnRowA && pColRemoveB == pawnColA) ||
                    (pRowRemoveB > board.GetLength(0) - 1 || pColRemoveB > board.GetLength(1) - 1) ||
                    (board[pRowRemoveB,pColRemoveB] == false))
            {
                WriteLine("Error!This tile cannot be removed.");
            }

                // This else statement says that if all the conditions are met then update the position of pawn B, remove the tile specified and then exit the loop
            else
            {
                pawnRowB = rowChangeB;
                pawnColB = colChangeB;
                board[pRowRemoveB, pColRemoveB] = false;
                isPossible = true;
            }
        }

        }
    }
}



