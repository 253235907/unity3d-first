import java.util.LinkedList;
import java.util.ArrayList;
import info.gridworld.grid.Location;
import info.gridworld.grid.AbstractGrid;
import info.gridworld.grid.Grid;

/**
 * A <code>SparseBoundedGrid</code> is a rectangular grid with a finite number of
 * rows and columns.It store occupant in a Arraylist <br />
 * The implementation of this class is testable on the AP CS AB exam.
 */
public class SparseBoundedGrid<E> extends AbstractGrid<E>
{
    private int gridRows;
    private int gridCols;

    //represent the node
    public class OccupantInCol
    {
        private Object occupant;
        private int col;
        public Object getO() {
            return occupant;
        }
        public int getC() {
            return col;
        }
        public OccupantInCol(E obj, int column) {
            occupant = obj;
            col = column;
        }
    }

    private ArrayList<LinkedList<OccupantInCol>> occupantArray;

    /**
     * Constructs an empty Sparsebounded grid with the given dimensions.
     * (Precondition: <code>rows > 0</code> and <code>cols > 0</code>.)
     * @param rows number of rows in SparseBoundedGrid
     * @param cols number of columns in SparseBoundedGrid
     */
    public SparseBoundedGrid(int rows, int cols)
    {
        if (rows <= 0)
            throw new IllegalArgumentException("rows <= 0");
        if (cols <= 0)
            throw new IllegalArgumentException("cols <= 0");
        occupantArray = new ArrayList<LinkedList<OccupantInCol>> (rows);
        for(int i = 0; i < rows; i++) {
            LinkedList<OccupantInCol> temp = new LinkedList<OccupantInCol> ();
            occupantArray.add(temp);
        }
        //Object obj = new Object();
        //occupantArray.get(0).get(0) = new Object;
        gridRows = rows;
        gridCols = cols;
    }

    public int getNumRows()
    {
        return gridRows;
    }

    public int getNumCols()
    {
        return gridCols;
    }

    public boolean isValid(Location loc)
    {
        return 0 <= loc.getRow() && loc.getRow() < getNumRows()
                && 0 <= loc.getCol() && loc.getCol() < getNumCols();
    }

    public ArrayList<Location> getOccupiedLocations()
    {
        ArrayList<Location> theLocations = new ArrayList<Location>();

        // Look at all grid locations.
        for(int i = 0; i < gridRows; i++) {
            for(int j = 0; j < occupantArray.get(i).size(); j++) {
                Location loc = new Location(i, occupantArray.get(i).get(j).getC());
                if(get(loc) != null)
                    theLocations.add(loc);
            }
        }

        return theLocations;
    }

    public E get(Location loc)
    {
        //if the positon is not in the list but in the grid, return null.
        int i = 0;
        //int check = 0;
        if (!isValid(loc))
            throw new IllegalArgumentException("Location " + loc
                    + " is not valid");
        if(occupantArray == null || occupantArray.size() <= loc.getRow()) return (E)null;
        if(occupantArray.get(loc.getRow()).size() == 0) return (E)null;
        for(i = 0; i < occupantArray.get(loc.getRow()).size(); i++) {
            if(occupantArray.get(loc.getRow()).get(i).getC() == loc.getCol()) {
                return (E)occupantArray.get(loc.getRow()).get(i).getO();
            }
        }
        return (E)null;
    }

    public E put(Location loc, E obj) {
        // TODO Auto-generated method stub
        if (!isValid(loc))
            throw new IllegalArgumentException("Location " + loc
                    + " is not valid");
        if (obj == null)
            throw new NullPointerException("obj == null");
        E oldOccupant = get(loc);
        OccupantInCol temp = new OccupantInCol(obj,loc.getCol());
        if(occupantArray.get(loc.getRow()).size() == 0) {
            occupantArray.get(loc.getRow()).add(temp);
        }
        else {
            int i;
            for(i = 0;i < occupantArray.get(loc.getRow()).size();i++) {
                if(occupantArray.get(loc.getRow()).get(i).getC() > loc.getCol()) {
                    break;
                }
            }
            occupantArray.get(loc.getRow()).add(i, temp);
        }
        return oldOccupant;
    }

    public E remove(Location loc)
    {
        if (!isValid(loc))
            throw new IllegalArgumentException("Location " + loc
                    + " is not valid");
        if(occupantArray == null || occupantArray.size() <= loc.getRow()) return get(loc);
        if(occupantArray.get(loc.getRow()).size() == 0) return get(loc);
        // Remove the object from the grid.
        E r = get(loc);
        int count = 0;
        for(int i = 0; i < occupantArray.get(loc.getRow()).size(); i++) {
            if(occupantArray.get(loc.getRow()).get(i).getC() == loc.getCol())
                occupantArray.get(loc.getRow()).remove(i);
        }
        //occupantArray.get(loc.getRow()).remove(count);
        return r;
    }
}
