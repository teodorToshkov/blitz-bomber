using UnityEngine;
using System.Collections;

/*! \brief This script spawns an instances of objects randomly on multiple postions per given intervals.\n

The difference at which random objects each position is 6 times the interval\n
When its time to spawn at a given position we pick randomly an object to spawn\n
\n
The objects have predefined probablilites to spawn as defined by the class ProbabilitySpawnerHelper\n
\n
The way the predefineed probabilities work is:
- we store the sum of the probabilities of each element;
- we generate a random number between zero and the sum;
- we set the object to instantiate to the last element;
	> the reason is that if we keep on subtracting probabilities\n
	> and we reach the last element but are left with a non-negative number\n
	> we will not have assigned an element to spawn where it would be obvious\n
	> that the one we should spawn is the last one\n

- if we imagine the probabilities as a in a stack (we have 4 elements with probabilities p0, p1, p2 and p3)
	and we pick a random number signed as "rand" equal to 3.4 we get:\verbatim
	(p0=1.1)  (p1=4.0)	 (p2=1.9)   (p3=3)
	_____|_____(rand)____|______|____________
	0	1	2	3	4	5	6	7	8	9	10
\endverbatim
- we increment through each probability and subtract each from the number as we go;\n
	if the difference we are left with is less than 0, it means we have found our element:\verbatim
	(p0=1.1)  (p1=4.0)	 (p2=1.9)   (p3=3)
	_____|_____(rand)____|______|____________		// start => rand = 3.4
	0	1	2	3	4	5	6	7	8	9	10
	↑

	(p0=1.1)  (p1=4.0)	 (p2=1.9)   (p3=3)
	_____|_____(rand)____|______|____________		// looking at p0 => rand = 2.3 (3.4 - p1)
	0	1	2	3	4	5	6	7	8	9	10
		 ↑

	(p0=1.1)  (p1=4.0)	 (p2=1.9)   (p3=3)
	_____|_____(rand)____|______|____________		// looking at p1 => rand = -1.7 (2.3 - p2)
	0	1	2	3	4	5	6	7	8	9	10
						 ↑\endverbatim
- as we see, when we are left with a negative number, this means we have found the number to instantiate.

## Why does it work?
When can pick a random number on the axis on which the probabilities lie
and the chance for picking any number is fixed and the same;\n
### In our example:
<ul>
	<li> __p1__ spans from 1.1 to 5.1 therefore if we were to pick only integers has __4__ possibibilities to be picked (numbers 2, 3, 4 and 5)</li>
	<li> __p0__ spans from 0 to 1.1 therefore has __2__ posibilities to be picked (numbers 0 and 1)</li>
	<li> __p2__ spans from 5.1 to 7 therefore has __2__ posibilities to be picked (numbers 6 and 7)</li>
	<li> __p3__ spans from 7 to 10 therefore has __3__ posibilities to be picked (numbers 8, 9 and 10)</li>
</ul>
As we see, the chance to pick __p1__ is __2__ times greater than that to pick __p0__ or __p2__
 */
public class Spawner : MonoBehaviour
{
	public ProbabilitySpawnerHelper[] targets; //!< objects to spawn with a given probability
	public float intervals = 1.75f; //!< intervals at which to spawn
	public Vector3[] positions; //!< positions at which to spawn

	private float targetsSumedProbability = 0; //!< the sumed probability of all probabilities the objects have (it is not necessarily 1)

	//! A function called at the instantiation of the class Spawner
	/*!
	 We initiate the time at which objects will start spawning at the given position
	 We calculate the sumed probability
	*/
	void Start()
	{
		float startDelay; // the time after which objects will start spawning at the given position
		// We loop through all positions to set the time at which it will start spawning objects
		foreach (Vector3 position in positions)
		{
			// We calculate a random time at which to start per fixed intervals defined by the intervals
			startDelay = intervals * (int)Random.Range (0, 6);
			// We set the first object to be spawn after that random time
			StartCoroutine (Spawn (position, startDelay));
		}
		// We calculate the maximum probability
		foreach (ProbabilitySpawnerHelper target in targets)
		{
			targetsSumedProbability += target.probability;
		}
	}

	//! A couroutine to spawn a random object at a given position after a given time
	IEnumerator Spawn (Vector3 position, float time)
	{
		// We wait for the time necessary
		yield return new WaitForSeconds (time);

		// We set the object to spawn to the last one
		Floor.Type newType = targets [targets.Length - 1].type;
		// We pick a random object to spawn
		float rand = Random.Range (0, targetsSumedProbability);
		// We loop through all targets to see which object we picked
		foreach (ProbabilitySpawnerHelper target in targets)
		{
			// if the object we picked is the one we are looking at, we say that and break from the loop
			rand -= target.probability;
			if (rand < 0)
			{
				newType = target.type;
				break;
			}
		}
		// We instatiate the object we picked and set the next one to spawn at the same position
		// after time equal to the interval devided by the rate at which the gameplay is going
		GameManager.AddFloor (newType, position);
		yield return Spawn (position, intervals / GameManager.gamePlaySpeed);
	}
}

[System.Serializable]
//! A helper class to hold objets to spawn along with defined probability they have to spawn
public class ProbabilitySpawnerHelper
{
	public Floor.Type type; //!< a _GameObject_ to be spawned
	public float probability; //!< the probability of that _GameObject_ to be spawned
}
