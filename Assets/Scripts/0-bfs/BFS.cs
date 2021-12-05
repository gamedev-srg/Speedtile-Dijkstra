using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

/**
 * A generic implementation of the BFS algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public class BFS {
    /** PLEASE DONT BE ALARMED BY THE SIZE OF THE CODE!
    * we had to implement the followong intefaces so the code will compile, 
    * Icomparable is for a node to be compared to other nodes,
    * IConvertible is so we could convert one type to the other - myNode to Nodetype
    * and ICloneable.
    */
    class myNode :IComparable , IConvertible,ICloneable{
        // node class that represent a node in the graph. each node has a vector (position in the world), and a wieght by the tile.
        Vector3Int node;
        float weight;
        public myNode(myNode other){
            node = other.node;
            weight = other.weight;
        }
        public myNode(Vector3Int vec,float w ){
            node = vec;
            weight = w;
        }
        public myNode(){
            node = new Vector3Int(0,0,0);
            weight = 0;
        }
        public int CompareTo(object other){
            myNode notme = other as myNode;
            if(node == notme.node && weight == notme.weight){
                return 1;
            }
            return 0;
        }
        public float getWeight(){
            return weight;
        }
        public void setWeight(float w){
            weight = w;
        }
        public Vector3Int getVector3Int(){
            return node;
        }
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(getVector3Int());
        }
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(getVector3Int());
        }
        TypeCode IConvertible.GetTypeCode(){
            return TypeCode.Object;
        }
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(getVector3Int());
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(getVector3Int());
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(getVector3Int());
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return getVector3Int().x;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(getVector3Int());
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(getVector3Int());
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(getVector3Int());
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(getVector3Int());
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(getVector3Int());
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return String.Format(getVector3Int().ToString());
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(getVector3Int(),conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(getVector3Int());
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(getVector3Int());
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(getVector3Int());
        }
        object ICloneable.Clone(){
            return new myNode(this);
        }

    }
    /**
    * This class represents a Priority Queue, because of the dotnet version in this project we had to implement it ourself.
    * the Queue uses two regular queues and find the lowest node.
    */
    class Pque{
        private Queue<myNode>  one;
        private Queue<myNode> two;
        public Pque(){
            one = new Queue<myNode>();
            two = new Queue<myNode>();
        }
        public void Enqueue(myNode node){
            one.Enqueue(node);
        }
        /** main function of the Queue we use the second queue to store the popped nodes, and to enqueue them back to queue number one.
        */
        public myNode Popmin(){
            myNode minnode = new myNode();
            float max = float.MaxValue;
            while(one.Count != 0){
                myNode temp = one.Dequeue();
                if(temp.getWeight() < max){
                    minnode = temp;
                    max = temp.getWeight();
                }
                two.Enqueue(temp);
            }
            while( two.Count !=0){ 
                myNode temp = two.Dequeue();
                if( minnode == temp){
                    continue;
                }else{
                    one.Enqueue(temp);
                }
            }
            return minnode;
        }
         public int Count(){
            return one.Count;
         }
    }
    private static Dictionary<string,float> tile_name = null;
    private static Tilemap tMap = null ;
    /** this function represents Dijkastra algorithm as requested.
    */
    public static void FindPath<NodeType>(
            IGraph<NodeType> graph, 
            NodeType startNode, NodeType endNode, 
            List<NodeType> outputPath,Tilemap tilemap,Dictionary<string,float> tname,
            int maxiterations=1000)
    {
        // setup of used variables.
        tMap = tilemap;
        tile_name = tname;
        Pque pque = new Pque();
        HashSet<NodeType> visited = new HashSet<NodeType>();
        Dictionary<myNode, myNode> prev = new Dictionary<myNode, myNode>();
        myNode current = new myNode(convert_NodeType(startNode), 0);
        pque.Enqueue(current);
        for (int i = 0; i < maxiterations; ++i) {
            if(pque.Count() == 0){ // we got to the end of the queue and didnt find a path, so we give up.
                break;
            }else{
                current = pque.Popmin(); // get the minimum weighted node
                NodeType src = (NodeType)Convert.ChangeType(current ,typeof(NodeType)); // convert the src and end, src is used as current as well.
                Vector3Int cur = convert_NodeType(src);
                Vector3Int end = convert_NodeType(endNode);
                if( cur == end ){// we found the destination node so we start to reconstruct the path (erels code.)
                    outputPath.Add(src);
                    while(prev.ContainsKey(current)){
                        current = prev[current];
                        src = (NodeType)Convert.ChangeType(current ,typeof(NodeType));
                        outputPath.Add(src);
                    }
                    outputPath.Reverse();
                    break;
                }else{
                    TileBase currspeed = TileOnPosition(convert_NodeType(current)); // based on erels code. find the tilebase so we can get the speed difiend in another file. 
                    foreach(var neighbor in graph.Neighbors(src)){ // for each nieghnor of the current node in the graph, check the wieght of the path up to them!
                        Vector3Int temp = convert_NodeType(neighbor);
                        TileBase tilespeed = TileOnPosition(temp);
                        myNode neighNode = new myNode(temp , tname[tilespeed.name]); // sets the wieght of the next node as the actual size.
                        if( ! visited.Contains(neighbor)){
                            visited.Add(neighbor);
                            neighNode = new myNode(temp , float.MaxValue);  // if the node is new, keep the wieght, else set the wieght as infinity to get the most effective one.
                        }
                        float tempDistance = current.getWeight() + tname[tilespeed.name]; // tempDistance represents the distance thus far.
                        if( tempDistance < neighNode.getWeight()){ // if tempdistance is lowe then the wieght we found a btter path so go there.
                            neighNode.setWeight(tempDistance);
                            pque.Enqueue(neighNode);
                            prev[neighNode] = current;
                            
                        }
                    }
                }
            }
        }
    }

    public static List<NodeType> GetPath<NodeType>(IGraph<NodeType> graph, NodeType startNode, NodeType endNode,Tilemap tilemap, Dictionary<string,float> at, int maxiterations=1000) {
        List<NodeType> path = new List<NodeType>();
        FindPath(graph, startNode, endNode, path, tilemap, at,maxiterations);
        return path;
    }
    // added function to convert between positions in the world to a tile type.
    private static TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tMap.WorldToCell(worldPosition);
        return tMap.GetTile(cellPosition);
    }
    //added function to convert between given NodeType to Vector3Int, meaning world position.
    private static Vector3Int convert_NodeType<NodeType> (NodeType node ){
            return (Vector3Int)Convert.ChangeType(node ,typeof(Vector3Int));
    }
}