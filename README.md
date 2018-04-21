import static org.junit.Assert.assertEquals;
import info.gridworld.actor.ActorWorld;
import info.gridworld.actor.Flower;
import info.gridworld.actor.Rock;
import info.gridworld.grid.Location;
import java.awt.Color;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

public class JumperTest {

    private ActorWorld world = new ActorWorld();
    private Jumper jumper = new Jumper();

    // set up the world
    @Before
    public void setUp() throws Exception {
        world.add(new Location(0, 0), jumper);
    }

    // test the bug act out grid side
    @Test
    public void testActOutGridside() {
        jumper.act();
        assertEquals(new Location(0, 0), jumper.getLocation());
        assertEquals(45, jumper.getDirection());

        jumper.moveTo(new Location(1, 0));
        jumper.setDirection(Location.NORTH);
        jumper.act();
        assertEquals(new Location(1, 0), jumper.getLocation());
        assertEquals(45, jumper.getDirection());
    }

    // test the bug move to the rock
    @Test
    public void testMoveToTheRock() {
        jumper.setDirection(Location.SOUTH);
        jumper.move();
        assertEquals(new Location(2, 0), jumper.getLocation());
        assertEquals(180, jumper.getDirection());
        assertEquals(1, world.getGrid().getOccupiedLocations().size());

    }

    // test move out of the grid side function
    @Test
    public void testMoveOutGridside() {
        // jump from the grid side
        jumper.moveTo(new Location(0, 0));
        jumper.setDirection(Location.NORTH);
        jumper.move();
        assertEquals(null, jumper.getLocation());
    }

    // test move() function
    @Test
    public void testMove() {
        // jump to east
        jumper.moveTo(new Location(0, 0));
        jumper.setDirection(Location.EAST);
        for (int i = 0; i < 4; i++) {
            Location target = new Location(0, i * 2);
            assertEquals(target, jumper.getLocation());
            jumper.move();
        }
        // out of the grid
        jumper.move();
        assertEquals(0, world.getGrid().getOccupiedLocations().size());
    }

    // test jump over the rock and flowers.
    @Test
    public void testJumper() {
        jumper.moveTo(new Location(4, 0));
        world.add(new Location(1, 0), new Rock());
        world.add(new Location(3, 0), new Flower());
        jumper.act();
        assertEquals(new Location(2, 0), jumper.getLocation());
        assertEquals(0, jumper.getDirection());
        jumper.act();
        assertEquals(new Location(0, 0), jumper.getLocation());
        assertEquals(0, jumper.getDirection());
    }

    // test move advanced
    @Test
    public void testMoveAdvanced() {
        jumper.moveTo(new Location(0, 0));
        // jump south east
        jumper.setDirection(Location.SOUTHEAST);
        for (int i = 0; i < 4; i++) {
            Location target = new Location(i * 2, i * 2);
            assertEquals(target, jumper.getLocation());
            jumper.move();
        }
        // out of the grid
        jumper.move();
        assertEquals(0, world.getGrid().getOccupiedLocations().size());
    }

    // test Jumper Constructor
    @Test
    public void testJumperConstructor() {
        assertEquals(Color.red, jumper.getColor());
    }

    // This case is to test the bug's act when the jumper is blocked by a
    // Rock.
    @Test
    public void testActCrashToBlock() {
        jumper.moveTo(new Location(4, 4));
        world.add(new Location(2, 4), new Rock());
        Location loc = new Location(4, 4);
        jumper.act();
        assertEquals(loc, jumper.getLocation());
        assertEquals(45, jumper.getDirection());
        loc = new Location(2, 6);
        jumper.act();
        assertEquals(loc, jumper.getLocation());
        assertEquals(45, jumper.getDirection());
    }

    // This case is to test the bug's act when the jumper is blocked by a
    // Flower.
    @Test
    public void testActCrashToFlower() {
        jumper.moveTo(new Location(4, 4));
        world.add(new Location(2, 4), new Flower());
        Location loc = new Location(4, 4);
        jumper.act();
        assertEquals(loc, jumper.getLocation());
        assertEquals(45, jumper.getDirection());
        loc = new Location(2, 6);
        jumper.act();
        assertEquals(loc, jumper.getLocation());
        assertEquals(45, jumper.getDirection());
    }

    // This case is to test when the jumper is trapped by rocks.
    @Test
    public void trappedByRocks() {
        jumper.moveTo(new Location(4, 4));
        world.add(new Location(2, 4), new Rock());
        world.add(new Location(4, 2), new Rock());
        world.add(new Location(4, 6), new Rock());
        world.add(new Location(6, 4), new Rock());
        world.add(new Location(2, 2), new Rock());
        world.add(new Location(2, 6), new Rock());
        world.add(new Location(6, 2), new Rock());
        world.add(new Location(6, 6), new Rock());
        Location loc = new Location(4, 4);
        for (int i = 0; i < 10; i++) {
            jumper.act();
        }
        assertEquals(loc, jumper.getLocation());
    }

    // After each test remove all object in the grid
    @After
    public void end() {
        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                world.remove(new Location(i, j));
            }
        }
    }

}














<?xml version="1.0" encoding="UTF-8" ?>
<project name="HelloWorld" default="run" basedir=".">
<property name="src" value="src"/>
<property name="dest" value="classes"/>
<property name="hello_jar" value="HelloWorld.jar"/>

<target name="junit">
	<available property="junit.present" classname="junit.framework.TestCase"/>
</target>

<target name="init" depends="junit">
   <mkdir dir="${dest}"/>
</target>

<target name="compile" depends="init">
   <javac srcdir="${src}" destdir="${dest}"/>
</target>

<target name="run" depends="compile">
   <java classname="HelloWorld" classpath="${dest}"/>
</target>

<target name="test" depends="compile">
	<junit printsummary="yes">
		<formatter type="plain"/>
		<classpath path="${dest}"/>
		<test name="HelloWorldTest"/>
	</junit>
</target>
</project>

public class HelloWorld {
	public static void main(String[] args) {
		System.out.println("Hello World!");
	}
	public int add(int x, int y) {
		int res = x + y;
		return res;
	}
	public String str() {
		return "Hello World";
	}
};


import junit.framework.Test;
import junit.framework.TestCase;
import junit.framework.TestSuite;

public class HelloWorldTest extends TestCase{
	public void testgetInt() {
		HelloWorld t1 = new HelloWorld();
		assertEquals(5, t1.add(2, 3));
	}
	public void teststr() {
		HelloWorld t2 = new HelloWorld();
		assertEquals("Hello_World", t2.str());
	}
}



