using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapGenerator : MonoBehaviour
{
    public int distance;
    public int minRoomWidth;
    public int minRoomHeight;

    public DivideSpace divideSpace;
    public SpreadTileMap spreadTileMap;

    public Player player;
    public GameObject enter;

    public HashSet<Vector2Int> floor;
    public HashSet<Vector2Int> wall;

    public GameObject enemy1;
    public GameObject enemy2;
    public Transform enemyzone;
    public Transform bulletzone;
    public Transform pickupzone;
    public GameObject home;

    public GameManager gm;

    public GameObject BossRoom1;
    public GameObject Boss1;
    public GameObject BossRoom2;
    public GameObject Boss2;

    public SoundManager sm;


    void Start()
    {
        //StartRandomMap();
        sm.PlayHomeSound();
    }

    public void StartRandomMap()
    {
        BossRoom1.SetActive(false);
        BossRoom2.SetActive(false);
        home.SetActive(false);
        spreadTileMap.ClearAllTiles();
        sm.PlayDungeonSound();
        if (gm.stage % 5 == 4 &&!(gm.stage==0))
        {
            sm.PlayBossSound();
            int ran = Random.Range(0, 2);
            if(ran == 0) {
                player.transform.position = new Vector2(0, 0);
                enter.transform.position = new Vector2(10, 0);
                GameObject enemyBoss = Instantiate(Boss1, new Vector2(0, 10), transform.rotation, enemyzone);

                Enemy_Boss enemyscript = enemyBoss.GetComponent<Enemy_Boss>();
                enemyscript.gm = gm;
                enemyscript.maxhp += gm.stage * 1000;
                enemyscript.curhp += gm.stage * 1000;
                enemyscript.player = player;
                enemyscript.damage = 10 + gm.stage / 5;
                enemyscript.bulletparent = bulletzone;
                enemyscript.pickparent = pickupzone;

                CircleCollider2D coll = enemyBoss.GetComponent<CircleCollider2D>();

                BossRoom1.SetActive(true);

                return;
            }
            else 
            {
                player.transform.position = new Vector2(0, 0);
                enter.transform.position = new Vector2(10, 0);
                GameObject enemyBoss = Instantiate(Boss2, new Vector2(0, 10), transform.rotation, enemyzone);

                Enemy_Boss2 enemyscript = enemyBoss.GetComponent<Enemy_Boss2>();
                enemyscript.gm = gm;
                enemyscript.maxhp += gm.stage * 1000;
                enemyscript.curhp += gm.stage * 1000;
                enemyscript.player = player;
                enemyscript.damage = 10 + gm.stage / 5;
                enemyscript.bulletparent = bulletzone;
                enemyscript.pickparent = pickupzone;

                CircleCollider2D coll = enemyBoss.GetComponent<CircleCollider2D>();

                BossRoom2.SetActive(true);

                return;
            }


        }
        for (int i = 0; i < enemyzone.childCount; i++)
        {
            Destroy(enemyzone.GetChild(i).gameObject);
        }

        for (int i = 0; i < bulletzone.childCount; i++) {
            Destroy(bulletzone.GetChild(i).gameObject);
        }
        for (int i = 0; i < pickupzone.childCount; i++)
        {
            Destroy(pickupzone.GetChild(i).gameObject);
        }



        divideSpace.totalSpace = new RectangleSpace(new Vector2Int(0, 0), divideSpace.totalWidth, divideSpace.totalHeight);
        divideSpace.spaceList = new List<RectangleSpace>();

        floor = new HashSet<Vector2Int>();
        wall = new HashSet<Vector2Int>();
        divideSpace.DivideRoom(divideSpace.totalSpace);

        MakeRandomRooms();
        MakeCorridors();
        MakeWall();


        spreadTileMap.SpreadFloorTilemap(floor);
        spreadTileMap.SpreadWallTilemap(wall);

        player.transform.position = (Vector2)divideSpace.spaceList[0].Center();
        enter.transform.position = (Vector2)divideSpace.spaceList[divideSpace.spaceList.Count - 1].Center();


    }



    public void MakeRandomRooms() {

        foreach (var space in divideSpace.spaceList) {
            HashSet<Vector2Int> positions = MakeRandomRectangle(space);
            floor.UnionWith(positions);
        }
    }

    public HashSet<Vector2Int> MakeRandomRectangle(RectangleSpace space) {

        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        int width = Random.Range(minRoomWidth, space.width + 1 - distance * 2);
        int height = Random.Range(minRoomHeight, space.height + 1 - distance * 2);

        for (int i = space.Center().x - width / 2; i <= space.Center().x + width / 2; i++) {

            for (int j = space.Center().y - height / 2; j <= space.Center().y + height / 2; j++) {

                int stageLevel = gm.stage / 10;
                if (stageLevel >= 10) {
                    stageLevel = 9;
                }
                int rannum = Random.Range(0, 150-stageLevel*10);
                if (rannum == 0) {
                    GameObject enemyin = Instantiate(enemy1,enemyzone);
                    
                    Enemy enemyscript = enemyin.GetComponent<Enemy>();
                    enemyscript.gm = gm;
                    enemyscript.maxhp += gm.stage * 10;
                    enemyscript.curhp += gm.stage * 10;
                    enemyscript.damage = 10 + gm.stage/5;
                    enemyscript.bulletparent = bulletzone;
                    enemyscript.pickparent = pickupzone;


                    enemyin.transform.position = new Vector2(i+0.5f, j+0.5f);


                }
                if (rannum == 1)
                {
                    GameObject enemyin = Instantiate(enemy2, enemyzone);

                    Enemy enemyscript = enemyin.GetComponent<Enemy>();
                    enemyscript.gm = gm;
                    enemyscript.maxhp += gm.stage * 10;
                    enemyscript.curhp += gm.stage * 10;

                    enemyscript.damage = 5 + gm.stage / 10;
                    enemyscript.pickparent = pickupzone;


                    enemyin.transform.position = new Vector2(i + 0.5f, j + 0.5f);


                }
                positions.Add(new Vector2Int(i, j));

            }

        }
        return positions;
    }


    public void MakeCorridors() {

        List<Vector2Int> tempCenter = new List<Vector2Int>();
        foreach (var space in divideSpace.spaceList) {

            tempCenter.Add(new Vector2Int(space.Center().x, space.Center().y));
        }

        Vector2Int nextCenter;
        Vector2Int currentCenter = tempCenter[0];
        tempCenter.Remove(currentCenter);
        while (tempCenter.Count != 0) {

            nextCenter = ChooseShortestNextCorridor(tempCenter, currentCenter);
            MakeOneCorridor(currentCenter, nextCenter);
            currentCenter = nextCenter;
            tempCenter.Remove(currentCenter);
        }
    }


    public Vector2Int ChooseShortestNextCorridor(List<Vector2Int> tempCenter, Vector2Int previousCenter) {

        int n = 0;
        float minLenght = float.MaxValue;

        for (int i = 0; i < tempCenter.Count; i++) {
            if (Vector2.Distance(previousCenter, tempCenter[i]) < minLenght) {
                minLenght = Vector2.Distance(previousCenter, tempCenter[i]);
                n = i;

            }
        }
        return tempCenter[n];
    }

    public void MakeOneCorridor(Vector2Int currentCenter, Vector2Int nextCenter) {

        Vector2Int current = new Vector2Int(currentCenter.x, currentCenter.y);
        Vector2Int next = new Vector2Int(nextCenter.x, nextCenter.y);
        floor.Add(current);

        while (current.x != next.x) {

            if (current.x < next.x)
            {
                current.x += 1;
                floor.Add(current);
            }
            else {

                current.x -= 1;
                floor.Add(current);
            }
        }

        while (current.y != next.y)
        {

            if (current.y < next.y)
            {
                current.y += 1;
                floor.Add(current);
            }
            else
            {

                current.y -= 1;
                floor.Add(current);
            }
        }


    }


    public void MakeWall() {

        foreach (Vector2Int tile in floor) {

            HashSet<Vector2Int> boundary = Make3X3Square(tile);
            boundary.ExceptWith(floor);
            if (boundary.Count != 0) {

                wall.UnionWith(boundary);
            }
        }
    
    }

    public HashSet<Vector2Int> Make3X3Square(Vector2Int tile) {

        HashSet<Vector2Int> boundary = new HashSet<Vector2Int>();
        for (int i = tile.x - 1; i <= tile.x + 1; i++) {
            for (int j = tile.y - 1; j <= tile.y + 1; j++)
            {
                boundary.Add(new Vector2Int(i, j));
            }

        }
        return boundary;
    
    }




    /*
    [SerializeField] private int width;
    [SerializeField] private int height;

    private int[,] map;

    private const int ROAD = 0;
    private const int WALL = 1;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;

    [SerializeField] private Color[] colors;

    private void Update()
    {
        Debug.Assert(!(width % 2 == 0 || height % 2 == 0), "홀수로 입력하십시오.");
        if (Input.GetMouseButtonDown(0)) Generate();
    }

    private void Generate()
    {
        map = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                OnDrawTile(x, y); //맵 크기에 맞춰 타일맵 배치
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 1 && y == 0) map[x, y] = ROAD; //시작 위치
                else if (x == width - 2 && y == height - 1) map[x, y] = ROAD; //출구 위치
                else if (x == 0 || x == width - 1 || y == 0 || y == height - 1) map[x, y] = WALL; //가장자리 벽으로 채움
                else if (x % 2 == 0 || y % 2 == 0) map[x, y] = WALL; //짝수 칸 벽으로 채움
                else map[x, y] = ROAD; //나머지 칸에는 길 배치
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos;
                if (x % 2 == 0 || y % 2 == 0) continue; //짝수 칸은 건너 뜀
                if (x == width - 2 && y == height - 2) continue; //우측 상단 모서리에 닿으면 길을 생성하지 않음
                if (x == width - 2) pos = new Vector2Int(x, y + 1); //오른쪽 끝에 닿으면 길 생성 방향을 위로 설정
                else if (y == height - 2) pos = new Vector2Int(x + 1, y); //위쪽 끝에 닿으면 길 생성 방향을 오른쪽으로 설정
                else if (Random.Range(0, 2) == 0) pos = new Vector2Int(x + 1, y); //랜덤으로 방향 지정 (위쪽, 오른쪽)
                else pos = new Vector2Int(x, y + 1);
                map[pos.x, pos.y] = ROAD; //맵 데이터에 값 저장
                SetTileColor(pos.x, pos.y); //타일맵 색상 변경
            }
        }
    }

    private void SetTileColor(int x, int y)
    {
        Vector3Int pos = new Vector3Int(-width / 2 + x, -height / 2 + y, 0); //생성 위치를 화면 중앙으로 설정
        tilemap.SetTileFlags(pos, TileFlags.None); //타일맵의 색상을 변경하기 위해 TileFlags값을 None으로 변경
        switch (map[x, y])
        {
            case ROAD: tilemap.SetColor(pos, colors[0]); break;
            case WALL: tilemap.SetColor(pos, colors[1]); break;
        }
    }

    private void OnDrawTile(int x, int y)
    {
        Vector3Int pos = new Vector3Int(-width / 2 + x, -height / 2 + y, 0);
        tilemap.SetTile(pos, tile);
    }*/


}


