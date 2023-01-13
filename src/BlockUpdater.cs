

    using System;

    /**
     * Singleton class that updates the number of blocks moved correctly or incorrectly
     */
    public class BlockUpdater
    {
        //total blocks generated
        private int _generatedBlock;
        //total blocks moved
        private int _totalBlocks;
        //blocks moved correctly
        private int _goalBlocks;
        //blocks moved incorrectly
        private int _errorBlocks;
      
        
        private static readonly Lazy<BlockUpdater> _instance =
            new Lazy<BlockUpdater>(() => new BlockUpdater());

        public static BlockUpdater Instance => _instance.Value;
        private BlockUpdater()
        {
            ResetAll();
        }

        public void ResetAll()
        {
            _totalBlocks = 0;
            _goalBlocks = 0;
            _errorBlocks = 0;
            _generatedBlock = 0;

        }

        public void AddGoalBlocks()
        {
            _goalBlocks = _goalBlocks +1;
            UpdateTotalBlocks();
        }
        
     
        
        public void UpdateTotalBlocks()
        {
            _totalBlocks = _errorBlocks + _goalBlocks ;
        }
        
        public void AddErrorBlocks()
        {
            _errorBlocks = _errorBlocks + 1;
            UpdateTotalBlocks();
        }
        
        public void AddGeneratedBlocks(int blocks)
        {
            _generatedBlock = _generatedBlock + blocks;
            
        }

        public int GeneratedBlock => _generatedBlock;

        public int TotalBlocks => _totalBlocks;

        public int GoalBlocks => _goalBlocks;

        public int ErrorBlocks => _errorBlocks;
    }
